/*!
    photobox v1.8.5
    (c) 2013 Yair Even Or <http://dropthebit.com>

    Uses jQuery-mousewheel Version: 3.0.6 by:
    (c) 2009 Brandon Aaron <http://brandonaaron.net>

    MIT-style license.
*/

(function($, doc, win){
    "use strict";
    var Photobox, photoboxes = [], photobox, options, images=[], imageLinks, activeImage = -1, activeURL, lastActive, activeType, prevImage, nextImage, thumbsStripe, docElm, APControl,
        transitionend = "transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd",
        isOldIE = !('placeholder' in doc.createElement('input')),
        noPointerEvents = (function(){ var el = $('<p>')[0]; el.style.cssText = 'pointer-events:auto'; return !el.style.pointerEvents})(),
        isMobile = 'ontouchend' in doc,
        thumbsContainerWidth, thumbsTotalWidth, activeThumb = $(),
        blankImg = "data:image/gif;base64,R0lGODlhAQABAIAAAP///////yH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==",
        transformOrigin = getPrefixed('transformOrigin'),
        transition = getPrefixed('transition'),

        // Preload images
        preload = {}, preloadPrev = new Image(), preloadNext = new Image(),
        // DOM elements
        closeBtn, image, video, prevBtn, nextBtn, caption, captionText, pbLoader, autoplayBtn, thumbs, wrapper,

        defaults = {
            single:     false,  // if "true" - gallery will only show a single image, with no way to navigate
            beforeShow: null,   // Callback before showing an image
            afterClose: null,   // Callback after closing the gallery
            loop:       true,   // Allows to navigate between first and last images
            thumb:      null,   // A relative path from the link to the thumbnail (if it's not inside the link)
            thumbs:     true,   // Show gallery thumbnails below the presented photo
            counter:    "(A/B)",   // Counts which piece of content is being viewed, relative to the total count of items in the photobox set. ["false","String"]
            title:      true,   // show the original alt or title attribute of the image's thumbnail
            autoplay:   false,  // should autoplay on first time or not
            time:       3000,   // autoplay interval, in miliseconds (less than 1000 will hide the autoplay button)
            history:    true,   // should use history hashing if possible (HTML5 API)
            hideFlash:  true,   // Hides flash elements on the page when photobox is activated. NOTE: flash elements must have wmode parameter set to "opaque" or "transparent" if this is set to false
            zoomable:   true,   // disable/enable mousewheel image zooming
            keys: {
                close: '27, 88, 67',    // keycodes to close photobox, default: esc (27), 'x' (88), 'c' (67)
                prev:  '37, 80',        // keycodes to navigate to the previous image, default: Left arrow (37), 'p' (80)
                next:  '39, 78'         // keycodes to navigate to the next image, default: Right arrow (39), 'n' (78)
            }
        },

        // DOM structure
        overlay =   $('<div id="pbOverlay">').append(
						'<input type="checkbox" id="pbThumbsToggler" checked hidden>',
                        pbLoader = $('<div class="pbLoader"><b></b><b></b><b></b></div>'),
                        prevBtn = $('<div id="pbPrevBtn" class="prevNext"><b></b></div>').on('click', next_prev),
                        nextBtn = $('<div id="pbNextBtn" class="prevNext"><b></b></div>').on('click', next_prev),
                        wrapper = $('<div class="wrapper">').append(  // gives Perspective
                            image = $('<img>'),
                        	video = $('<div>')
                        ),
                        closeBtn = $('<div id="pbCloseBtn">').on('click', close)[0],
                        autoplayBtn = $('<div id="pbAutoplayBtn">').append(
                            $('<div class="pbProgress">')
                        ),
                        caption = $('<div id="pbCaption">').append(
							'<label for="pbThumbsToggler" title="thumbnails on/off"></label>',
                            captionText = $('<div class="pbCaptionText">').append('<div class="title"></div><div class="counter">'),
                            thumbs = $('<div>').addClass('pbThumbs')
                        )
                    );
    /*---------------------------------------------------------------
        Initialization (on DOM ready)
    */
    function prepareDOM(){
        // if useragent is IE < 10 (user deserves a slap on the face, but I gotta support them still...)
        isOldIE && overlay.addClass('msie');

        noPointerEvents && overlay.hide();

        autoplayBtn.off().on('click', APControl.toggle);
        // attach a delegated event on the thumbs container
        thumbs.off().on('click', 'a', thumbsStripe.click);
        // enable scrolling gesture on mobile
        isMobile && thumbs.css('overflow', 'auto');

        // cancel prppogation up to the overlay container so it won't close
        overlay.off().on('click', 'img', function(e){
            e.stopPropagation();
        });

        $(doc.body).append(overlay);

        // need this for later:
        docElm = doc.documentElement;
    }

    // @param [List of elements to work on, Custom settings, Callback after image is loaded]
    $.fn.photobox = function(target, settings, callback){
        return this.each(function(){
            var o, pb, 
				PB_data = $(this).data('_photobox');
			
    		if( PB_data ){ // don't initiate the plugin more than once on the same element
    			if( target === 'destroy')
					PB_data.destroy();
					
				return this;
			}

            if( typeof target != 'string' )
                target = 'a';

            if( target === 'prepareDOM' ){
                prepareDOM();
    			return this;
    		}

            o = $.extend({}, defaults, settings || {});
            pb = new Photobox(o, this, target);

            // Saves the insance on the gallery's target element
            $(this).data('_photobox', pb);
            // add a callback to the specific gallery
            pb.callback = callback;
            // save every created gallery pointer
            photoboxes.push( pb );
        });
    }

    Photobox = function(_options, object, target){
        this.options = $.extend({}, _options);
        this.target = target;
        this.selector = $(object || doc);

        this.thumbsList = null;
        // filter the links which actually HAS an image as a child
        var filtered = this.imageLinksFilter( this.selector.find(target) );

        this.imageLinks = filtered[0];  // Array of jQuery links
        this.images = filtered[1];      // 2D Array of image URL & title
		this.init();
    };

    Photobox.prototype = {
        init : function(){
            var that = this;

            // only generates the thumbStripe once, and listen for any DOM changes on the selector element, if so, re-generate
            if( this.options.thumbs ){
                // generate gallery thumbnails every time (because links might have changed)
                this.thumbsList = thumbsStripe.generate(this.imageLinks);
			}

            this.selector.on('click.photobox', this.target, function(e){
                e.preventDefault();
                that.open(this);
            });

            // if any node was added or removed from the Selector of the gallery
            this.observerTimeout = null;

            if( this.selector[0].nodeType == 1 ) // observe normal nodes
                that.observeDOM( that.selector[0], function(){
                    // use a timeout to prevent more than one DOM change event firing at once, and also to overcome the fact that IE's DOMNodeRemoved is fired BEFORE elements were actually removed
                    clearTimeout(that.observerTimeout);
                    that.observerTimeout = setTimeout( function(){
                        var filtered = that.imageLinksFilter( that.selector.find(that.target) ),
                            activeIndex = 0;

                        that.imageLinks = filtered[0];
                        that.images = filtered[1];

                        // if photobox is opened
						if( photobox ){
                            // if gallery which was changed is the currently viewed one:
                            if( that.selector == photobox.selector ){
                                images = that.images;
                                imageLinks = that.imageLinks;

                                // check if the currently VIEWED photo has been detached from a photobox set
                                // if so, remove navigation arrows
                                // TODO: fix the "images" to be an object and not an array.
                                for( var i = images.length; i--; ){
                                    if( images[i][0] == activeURL )
                                        return;
                                    // if not exits any more
                                    overlay.removeClass('hasArrows');
                                }
                            }
                        }

                        // if this gallery has thumbs
                        if( that.options.thumbs ){
                            that.thumbsList = thumbsStripe.generate(that.imageLinks);
    						thumbs.html( that.thumbsList );
                        }

						if( that.images.length && activeURL && that.options.thumbs ){
							activeIndex = that.thumbsList.find('a[href="'+activeURL+'"]').eq(0).parent().index();
							updateIndexes(activeIndex);
                            thumbsStripe.changeActive(activeIndex, 0);
						}
                    }, 50);
                });
        },

        open : function(link){
            var startImage = $.inArray(link, this.imageLinks);
            // if image link does not exist in the imageLinks array (probably means it's not a valid part of the gallery)
            if( startImage == -1 )
			    return false;

            // load the right gallery selector...
            options = this.options;
            images = this.images;
            imageLinks = this.imageLinks;

            photobox = this;
            this.setup(1);

            overlay.on(transitionend, function(){
                overlay.off(transitionend).addClass('on'); // class 'on' is set when the initial fade-in of the overlay is done
                changeImage(startImage, true);
            }).addClass('show');

            if( isOldIE )
                overlay.trigger('MSTransitionEnd');

            return false;
        },

        imageLinksFilter : function(obj){
            var that = this,
                images = [],
                caption = {},
                captionlink;

            return [obj.filter(function(i){
                // search for the thumb inside the link, if not found then see if there's a 'that.settings.thumb' pointer to the thumbnail
                var link = $(this), img = link.find('img')[0] || link.find(that.options.thumb)[0];
                // if no img child found in the link
                if( img )
				    captionlink = img.getAttribute('data-pb-captionlink')

                caption.content = "<span>" +( img.getAttribute('alt') || img.getAttribute('title') || '') + "</span>";
                // if there is a caption link to be added:
				if( captionlink ){
					captionlink = captionlink.split('[');
					// parse complex links: text[www.site.com]
					if( captionlink.length == 2 ){
						caption.linkText = captionlink[0];
						caption.linkHref = captionlink[1].slice(0,-1);
					}
					else{
						caption.linkText = captionlink;
						caption.linkHref = captionlink;
					}
					caption.content += ' <a href="'+ caption.linkHref +'">' + caption.linkText + '</a>';
				}

                images.push([link[0].href, caption.content]);

                return true;
            }), images];
        },

        //check if DOM nodes were added or removed, to re-build the imageLinks and thumbnails
        observeDOM : (function(){
            var MutationObserver = win.MutationObserver || win.WebKitMutationObserver,
                eventListenerSupported = win.addEventListener;

            return function(obj, callback){
                if( MutationObserver ){
                    // define a new observer
                    var obs = new MutationObserver(function(mutations, observer){
                        if( mutations[0].addedNodes.length || mutations[0].removedNodes.length )
                            callback();
                    });
                    // have the observer observe foo for changes in children
                    obs.observe( obj, { childList:true, subtree:true });
                }
                else if( eventListenerSupported ){
                    obj.addEventListener('DOMNodeInserted', callback, false);
                    obj.addEventListener('DOMNodeRemoved', callback, false);
                }
            }
        })(),

        // things that should happen every time the gallery opens or closes (some messed up code below..)
        setup : function (open){
            var fn = open ? "on" : "off";

            // a hack to change the image src to nothing, because you can't do that in CHROME
            image[0].src = blankImg;

            // thumbs stuff
            if( options.thumbs ){
                if( !isMobile ){
                    thumbs[fn]('mouseenter.photobox', thumbsStripe.calc)
                          [fn]('mousemove.photobox', thumbsStripe.move);
                }
            }

            if( open ){
                image.css({'transition':'0s'}).removeAttr('style'); // reset any transition that might be on the element (yes it's ugly)
                overlay.show();
                // Clean up if another gallery was viewed before, which had a thumbsList
                thumbs
                    .html( this.thumbsList )
                    .trigger('mouseenter.photobox');


                overlay[options.thumbs ? 'addClass' : 'removeClass']('thumbs');

                // things to hide if there are less than 2 images
				console.log(this.images.length < 2 , options.single);
                if( this.images.length < 2 ||  options.single )
                    overlay.removeClass('thumbs hasArrows hasCounter hasAutoplay');
                else{
                    overlay.addClass('hasArrows hasCounter')

                    // check is the autoplay button should be visible (per gallery) and if so, should it autoplay or not.
                    if( options.time > 1000 ){
                        overlay.addClass('hasAutoplay');
                        if( options.autoplay )
                            APControl.progress.start();
                        else
                            APControl.pause();
                    }
                    else
                        overlay.removeClass('hasAutoplay');
                }

                options.hideFlash && $('iframe, object, embed').css('visibility', 'hidden');

            } else {
                $(win).off('resize.photobox');
            }

            $(doc).off("keydown.photobox")[fn]({ "keydown.photobox": keyDown });

            if( 'ontouchstart' in document.documentElement ){
                overlay.removeClass('hasArrows'); // no need for Arrows on touch-enabled
                wrapper[fn]('swipe', onSwipe);
            }

            if( options.zoomable ){
                overlay[fn]({"mousewheel.photobox": scrollZoom });
                if( !isOldIE) thumbs[fn]({"mousewheel.photobox": thumbsResize });
            }

            if( !options.single )
			    overlay[fn]({"mousewheel.photobox": wheelNextPrev });
        },

        destroy : function(){
			options = this.options;
            this.selector
                .off('click.photobox', this.target)
                .removeData('_photobox');

            close();
        }
    }

    // on touch-devices only
    function onSwipe(e, Dx, Dy){
        if( Dx == 1 ){
            image.css({transform:'translateX(25%)', transition:'.7s', opacity:0});
            setTimeout(function(){ changeImage(prevImage) }, 200);
        }
        else if( Dx == -1 ){
            image.css({transform:'translateX(-25%)', transition:'.7s', opacity:0});
            setTimeout(function(){ changeImage(nextImage) }, 200);
        }

        if( Dy == 1 )
            thumbs.addClass('show');
        else if( Dy == -1 )
            thumbs.removeClass('show');
    }

    // manage the (bottom) thumbs strip
    thumbsStripe = (function(){
		var containerWidth   = 0,
			scrollWidth      = 0,
			posFromLeft      = 0,    // Stripe position from the left of the screen
			stripePos        = 0,    // When relative mouse position inside the thumbs stripe
			animated         = null,
			padding,  				 // in percentage to the containerWidth
			el, $el, ratio, scrollPos, pos;

		return{
			// returns a <ul> element which is populated with all the gallery links and thumbs
			generate : function(imageLinks){
				var thumbsList = $('<ul>'), link, elements = [], i, len = imageLinks.size(), title, image, type;

				for( i = 0; i < len; i++ ){
					link = imageLinks[i];
					image = $(link).find('img');
					title = image[0].title || image[0].alt || '';
					type = link.rel ? " class='" + link.rel +"'" : '';
					elements.push('<li'+ type +'><a href="'+ link.href +'"><img src="'+ image[0].src +'" alt="" title="'+ title +'" /></a></li>');
				};

				thumbsList.html( elements.join('') );
				return thumbsList;
			},

			click : function(e){
				e.preventDefault();

				activeThumb.removeClass('active');
				activeThumb = $(this).parent().addClass('active');

				var imageIndex = $(this.parentNode).index();
				return changeImage(imageIndex, 0, 1);
			},

			changeActiveTimeout : null,
			/** Highlights the thumb which represents the photo and centres the thumbs viewer on it.
			**  @thumbClick - if a user clicked on a thumbnail, don't center on it
			*/
			changeActive : function(index, delay, thumbClick){
				var lastIndex = activeThumb.index();
				activeThumb.removeClass('active');
				activeThumb = thumbs.find('li').eq(index).addClass('active');
				if( thumbClick ) return;
				// set the scrollLeft position of the thumbs list to show the active thumb
				clearTimeout(this.changeActiveTimeout);
				// give the images time to to settle on their new sizes (because of css transition) and then calculate the center...
				this.changeActiveTimeout = setTimeout(
					function(){
						var pos = activeThumb[0].offsetLeft + activeThumb[0].clientWidth/2 - docElm.clientWidth/2;
						delay ? thumbs.delay(800) : thumbs.stop();
						thumbs.animate({scrollLeft: pos}, 500, 'swing');
					}, 200);
			},

			// calculate the thumbs container width, if the window has been resized
			calc : function(e){
				el = thumbs[0];

				containerWidth       = el.clientWidth;
				scrollWidth          = el.scrollWidth;
				padding 			 = 0.15 * containerWidth;

				posFromLeft          = thumbs.offset().left;
				stripePos            = e.pageX - padding - posFromLeft;
				pos                  = stripePos / (containerWidth - padding*2);
				scrollPos            = (scrollWidth - containerWidth ) * pos;

				thumbs.animate({scrollLeft:scrollPos}, 200);

				clearTimeout(animated);
				animated = setTimeout(function(){
					animated = null;
				}, 200);

				return this;
			},

			// move the stripe left or right according to mouse position
			move : function(e){
				// don't move anything until initial movement on 'mouseenter' has finished
				if( animated ) return;

				ratio     = scrollWidth / containerWidth;
				stripePos = e.pageX - padding - posFromLeft; // the mouse X position, "normalized" to the carousel position

				if( stripePos < 0) stripePos = 0; //

				pos       = stripePos / (containerWidth - padding*2); // calculated position between 0 to 1
				// calculate the percentage of the mouse position within the carousel
				scrollPos = (scrollWidth - containerWidth ) * pos;

				el.scrollLeft = scrollPos;
			}
		}
    })();

    // Autoplay controller
    APControl = {
        autoPlayTimer : false,
        play : function(){
            APControl.autoPlayTimer = setTimeout(function(){ changeImage(nextImage) }, options.time);
            APControl.progress.start();
            autoplayBtn.removeClass('play');
            APControl.setTitle('Click to stop autoplay');
            options.autoplay = true;
        },
        pause : function(){
            clearTimeout(APControl.autoPlayTimer);
            APControl.progress.reset();
            autoplayBtn.addClass('play');
            APControl.setTitle('Click to resume autoplay');
            options.autoplay = false;
        },
        progress : {
            reset : function(){
                autoplayBtn.find('div').removeAttr('style');
                setTimeout(function(){ autoplayBtn.removeClass('playing') },200);
            },
            start : function(){
                if( !isOldIE)
                    autoplayBtn.find('div').css(transition, options.time+'ms');
                autoplayBtn.addClass('playing');
            }
        },
        // sets the button Title property
        setTitle : function(text){
            if(text)
                autoplayBtn.prop('title', text + ' (every ' + options.time/1000 + ' seconds)' );
        },
        // the button onClick handler
        toggle : function(e){
            e.stopPropagation();
            APControl[ options.autoplay ? 'pause' : 'play']();
        }
    }

    function getPrefixed(prop){
        var i, s = doc.createElement('p').style, v = ['ms','O','Moz','Webkit'];
        if( s[prop] == '' ) return prop;
        prop = prop.charAt(0).toUpperCase() + prop.slice(1);
        for( i = v.length; i--; )
            if( s[v[i] + prop] == '' )
                return (v[i] + prop);
    }

    function keyDown(event){
        var code = event.keyCode, ok = options.keys, result;
        // Prevent default keyboard action (like navigating inside the page)
        return ok.close.indexOf(code) >= 0 && close() ||
               ok.next.indexOf(code) >= 0 && !options.single && changeImage(nextImage) ||
               ok.prev.indexOf(code) >= 0 && !options.single && changeImage(prevImage) || true;
    }

	function wheelNextPrev(e, dY, dX){
		if( dX == 1 )
			changeImage(nextImage);
		else if( dX == -1 )
			changeImage(prevImage);
	}

    // serves as a callback for pbPrevBtn / pbNextBtn buttons but also is called on keypress events
    function next_prev(){
        // don't get crazy when user clicks next or prev buttons rapidly
        //if( !image.hasClass('zoomable') )
        //  return false;

        var img = (this.id == 'pbPrevBtn') ? prevImage : nextImage;

        changeImage(img);
        return false;
    }

	function updateIndexes(idx){
		lastActive = activeImage;
        activeImage = idx;
        activeURL = images[idx][0];
        prevImage = (activeImage || (options.loop ? images.length : 0)) - 1;
        nextImage = ((activeImage + 1) % images.length) || (options.loop ? 0 : -1);
	}

    function changeImage(imageIndex, firstTime, thumbClick){
        if( !imageIndex || imageIndex < 0 )
            imageIndex = 0;

        // if there's a callback for this point:
        if( typeof options.beforeShow == "function")
            options.beforeShow(imageLinks[imageIndex]);

        overlay.removeClass('error').addClass( imageIndex > activeImage ? 'next' : 'prev' );

		updateIndexes(imageIndex);

		// reset things
        stop();
		video.empty();
		preload.onerror = null;
		image.add(video).data('zoom', 1);

		activeType = imageLinks[imageIndex].rel == 'video' ? 'video' : 'image';

		// check if current link is a video
		if( activeType == 'video' ){
			video.html( newVideo() ).addClass('hide');
			showContent(firstTime);
		}
        else{
			// give a tiny delay to the preloader, so it won't be showed when images are already cached
			var loaderTimeout = setTimeout(function(){ overlay.addClass('pbLoading'); },50);
			// hide/show next-prev buttons
			if( !options.loop ){
				nextBtn[ imageIndex == images.length-1 ? 'addClass' : 'removeClass' ]('hide');
				prevBtn[ imageIndex == 0 ? 'addClass' : 'removeClass' ]('hide');
			}

			if( prevImage >= 0 ) preloadPrev.src = images[prevImage][0];
			if( nextImage >= 0 ) preloadNext.src = images[nextImage][0];

			if( isOldIE ) overlay.addClass('hide'); // should wait for the image onload. just hide the image while old ie display the preloader

			options.autoplay && APControl.progress.reset();
			preload = new Image();
			preload.onload = function(){ clearTimeout(loaderTimeout); showContent(firstTime); };
			preload.onerror = imageError;
			preload.src = activeURL;
        }

		// Show Caption text
		captionText.on(transitionend, captionTextChange).addClass('change');
		if( firstTime || isOldIE ) captionTextChange();

		if( options.thumbs )
			thumbsStripe.changeActive(imageIndex, firstTime, thumbClick);
        // Save url hash for current image
        history.save();
    }

	function newVideo(){
		var url = images[activeImage][0],
			sign = $('<a>').prop('href',images[activeImage][0])[0].search ? '&' : '?';
		url += sign + 'vq=hd720&wmode=opaque';
		return $("<iframe>").prop({ scrolling:'no', frameborder:0, allowTransparency:true, src:url }).attr({webkitAllowFullScreen:true, mozallowfullscreen:true, allowFullScreen:true});
	}

	// show the item's Title & Counter
	function captionTextChange(){
		captionText.off(transitionend).removeClass('change');
		// change caption's text
		if( options.counter ){
			var value = options.counter.replace('A', activeImage + 1).replace('B', images.length);
			caption.find('.counter').text(value);
		}
		options.title && caption.find('.title').html( images[activeImage][1] );
	}

    // Handles the history states when changing images
    var history = {
        save : function(){
            // only save to history urls which are not already in the hash
            if('pushState' in window.history && decodeURIComponent(window.location.hash.slice(1)) != activeURL && options.history ){
                window.history.pushState( 'photobox', doc.title + '-' + images[activeImage][1], window.location.pathname + window.location.search + '#' + encodeURIComponent(activeURL) );
            }
        },
        load : function(){
            if( options && !options.history ) return false;
            var hash = decodeURIComponent( window.location.hash.slice(1) ), i, j;
            if( !hash && overlay.hasClass('show') )
                close();
            else
            // Scan all galleries for the image link (open the first gallery that has the link's image)
                for( i = 0; i < photoboxes.length; i++ )
                    for( j in photoboxes[i].images )
                        if( photoboxes[i].images[j][0] == hash ){
                            photoboxes[i].open( photoboxes[i].imageLinks[j] );
                            return true;
                        }
        },
        clear : function(){
            if( options.history && 'pushState' in window.history )
                window.history.pushState('photobox', doc.title, window.location.pathname + window.location.search);
        }
    };

    // Add Photobox special `onpopstate` to the `onpopstate` function
    window.onpopstate = (function(){
        var cached = window.onpopstate;
        return function(event){
            cached && cached.apply(this, arguments);
            if( event.state == 'photobox' )
                history.load();
        }
    })();

    // handles all image loading error (if image is dead)
    function imageError(){
        overlay.addClass('error');
        image[0].src = blankImg; // set the source to a blank image
        preload.onerror = null;
    }

	// Shows the content (image/video) on the screen
    function showContent(firstTime){
		var out, showSaftyTimer;
		showSaftyTimer = setTimeout(show, 2000);

		overlay.removeClass("pbLoading").addClass('hide');
        image.add(video).removeAttr('style').removeClass('zoomable'); // while transitioning an image, do not apply the 'zoomable' class

		// check which element needs to transition-out:
		if( !firstTime && imageLinks[lastActive].rel == 'video' ){
			out = video;
			image.addClass('prepare');
		}
		else
			out = image;

        if( firstTime || isOldIE )
            show();
        else
            out.on(transitionend, show);

		// in case the 'transitionend' didn't fire
        // after hiding the last seen image, show the new one
        function show(){
			clearTimeout(showSaftyTimer);
			out.off(transitionend).css({'transition':'none'});
			overlay.removeClass('video');
			if( activeType == 'video' ){
				image[0].src = blankImg;
				video.addClass('prepare');
				overlay.addClass('video');
			}
			else
				image.prop({ 'src':activeURL, 'class':'prepare' });

			// filthy hack for the transitionend event, but cannot work without it:
			setTimeout(function(){
				image.add(video).removeAttr('style').removeClass('prepare');
				overlay.removeClass('hide next prev');
				setTimeout(function(){
					image.add(video).on(transitionend, showDone);
					if(isOldIE) showDone(); // IE9 and below don't support transitionEnd...
				}, 0);
			},50);
        }
    }

	// a callback whenever a transition of an image or a video is done
    function showDone(){
        image.add(video).off(transitionend).addClass('zoomable');
		if( activeType == 'video' )
			video.removeClass('hide');
		else
			autoplayBtn && options.autoplay && APControl.play();
        if( typeof photobox.callback == 'function' )
            photobox.callback.apply(imageLinks[activeImage]);
    }

    function scrollZoom(e, deltaY, deltaX){
		if( deltaX ) return false;

		if( activeType == 'video' ){
			var zoomLevel = video.data('zoom') || 1;
			zoomLevel += (deltaY / 10);
			if( zoomLevel < 0.5 )
				return false;

			video.data('zoom', zoomLevel).css({width:624*zoomLevel, height:351*zoomLevel});
		}
		else{
			var zoomLevel = image.data('zoom') || 1,
				getSize = image[0].getBoundingClientRect();

			zoomLevel += (deltaY / 10);

			if( zoomLevel < 0.1 )
				zoomLevel = 0.1;

			image.data('zoom', zoomLevel).css({'transform':'scale('+ zoomLevel +')'});

			// check if dragging should take effect (if image is larger than the window
			if( getSize.height > docElm.clientHeight || getSize.width > docElm.clientWidth ){
				$(doc).on('mousemove.photobox', imageReposition);
			}
			else{
				$(doc).off('mousemove.photobox');
				image[0].style[transformOrigin] = '50% 50%';
			}
        }
        return false;
    }

    function thumbsResize(e, delta){
        e.preventDefault();
        e.stopPropagation(); // stop the event from bubbling up to the Overlay and enlarge the content itself
        var thumbList = photobox.thumbsList;
        thumbList.css('height', thumbList[0].clientHeight + (delta * 10) );
        var h = caption[0].clientHeight / 2;
        wrapper[0].style.cssText = "margin-top: -"+ h +"px; padding: "+ h +"px 0;";
        thumbs.hide().show(0);
        thumbsStripe.calc();
    }

    // moves the image around during zoom mode on mousemove event
    function imageReposition(e){
        var y = (e.clientY / docElm.clientHeight) * (docElm.clientHeight + 200) - 100, // extend the range of the Y axis by 100 each side
            yDelta = y / docElm.clientHeight * 100,
            xDelta = e.clientX / docElm.clientWidth * 100,
            origin = xDelta.toFixed(2)+'% ' + yDelta.toFixed(2) +'%';

        image[0].style[transformOrigin] = origin;
    }

    function stop(){
        clearTimeout(APControl.autoPlayTimer);
        $(doc).off('mousemove.photobox');
        preload.onload = function(){};
        preload.src = preloadPrev.src = preloadNext.src = activeURL;
    }

    function close(){
            stop();
			video.find('iframe').prop('src','').empty();
            Photobox.prototype.setup();
            history.clear();

            overlay.removeClass('on video').addClass('hide');

            image.on(transitionend, hide);
            isOldIE && hide();

            function hide(){
                if( overlay[0].className == '' ) return; // if already hidden
                overlay.removeClass('show hide error pbLoading');
                image.removeAttr('class').removeAttr('style').off().data('zoom',1);

                if(noPointerEvents) // pointer-events lack support in IE, so just hide the overlay
                    setTimeout(function(){ overlay.hide(); }, 200);

                options.hideFlash && $('iframe, object, embed').css('visibility', 'visisble');
            }

            // fall-back if the 'transitionend' event didn't fire
            setTimeout(hide, 500);
            // callback after closing the gallery
            if( typeof options.afterClose === 'function' )
                options.afterClose(overlay);
    }


    /*! Copyright (c) 2011 Brandon Aaron (http://brandonaaron.net)
     * Licensed under the MIT License (LICENSE.txt).
     *
     * Version: 3.0.6
     */
    var types = ['DOMMouseScroll', 'mousewheel'];

    if ($.event.fixHooks){
        for ( var i=types.length; i; )
            $.event.fixHooks[ types[--i] ] = $.event.mouseHooks;
    }

    $.event.special.mousewheel = {
        setup: function(){
            if( this.addEventListener ){
                for ( var i=types.length; i; )
                    this.addEventListener( types[--i], handler, false );
            }else
                this.onmousewheel = handler;
        },
        teardown: function(){
            if ( this.removeEventListener ){
                for ( var i=types.length; i; )
                    this.removeEventListener( types[--i], handler, false );
            }else
                this.onmousewheel = null;
        }
    };

    $.fn.extend({
        mousewheel: function(fn){
            return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
        },
        unmousewheel: function(fn){
            return this.unbind("mousewheel", fn);
        }
    });


    function handler(event){
        var orgEvent = event || win.event, args = [].slice.call( arguments, 1 ), delta = 0, returnValue = true, deltaX = 0, deltaY = 0;
        event = $.event.fix(orgEvent);
        event.type = "mousewheel";

        // Old school scrollwheel delta
        if( orgEvent.wheelDelta ){ delta = orgEvent.wheelDelta/120; }
        if( orgEvent.detail     ){ delta = -orgEvent.detail/3; }

        // New school multidimensional scroll (touchpads) deltas
        deltaY = delta;

        // Gecko
        if( orgEvent.axis !== undefined && orgEvent.axis === orgEvent.HORIZONTAL_AXIS ){
            deltaY = 0;
            deltaX = -1*delta;
        }

        // Webkit
        if( orgEvent.wheelDeltaY !== undefined ){ deltaY = orgEvent.wheelDeltaY/120; }
        if( orgEvent.wheelDeltaX !== undefined ){ deltaX = -1*orgEvent.wheelDeltaX/120; }

        // Add event and delta to the front of the arguments
        args.unshift(event, delta, deltaX, deltaY);
        return ($.event.dispatch || $.event.handle).apply(this, args);
    }

	/*! Copyright (c) 2013 Brandon Aaron (http://brandon.aaron.sh)
	 * Licensed under the MIT License (LICENSE.txt).
	 *
	 * Version: 3.1.9
	 *
	 * Requires: jQuery 1.2.2+
	 */

	(function(){
		var toFix  = ['wheel', 'mousewheel', 'DOMMouseScroll', 'MozMousePixelScroll'],
			toBind = ( 'onwheel' in document || document.documentMode >= 9 ) ?
						['wheel'] : ['mousewheel', 'DomMouseScroll', 'MozMousePixelScroll'],
			slice  = Array.prototype.slice,
			nullLowestDeltaTimeout, lowestDelta;

		if ( $.event.fixHooks ) {
			for ( var i = toFix.length; i; ) {
				$.event.fixHooks[ toFix[--i] ] = $.event.mouseHooks;
			}
		}

		var special = $.event.special.mousewheel = {
			setup: function() {
				if ( this.addEventListener ) {
					for ( var i = toBind.length; i; ) {
						this.addEventListener( toBind[--i], handler, false );
					}
				} else {
					this.onmousewheel = handler;
				}
				// Store the line height and page height for this particular element
				$.data(this, 'mousewheel-line-height', special.getLineHeight(this));
				$.data(this, 'mousewheel-page-height', special.getPageHeight(this));
			},

			teardown: function() {
				if ( this.removeEventListener ) {
					for ( var i = toBind.length; i; ) {
						this.removeEventListener( toBind[--i], handler, false );
					}
				} else {
					this.onmousewheel = null;
				}
			},

			getLineHeight: function(elem) {
				return parseInt($(elem)['offsetParent' in $.fn ? 'offsetParent' : 'parent']().css('fontSize'), 10);
			},

			getPageHeight: function(elem) {
				return $(elem).height();
			},

			settings: {
				adjustOldDeltas: true
			}
		};

		$.fn.extend({
			mousewheel: function(fn) {
				return fn ? this.bind('mousewheel', fn) : this.trigger('mousewheel');
			},

			unmousewheel: function(fn) {
				return this.unbind('mousewheel', fn);
			}
		});


		function handler(event) {
			var orgEvent   = event || window.event,
				args       = slice.call(arguments, 1),
				delta      = 0,
				deltaX     = 0,
				deltaY     = 0,
				absDelta   = 0;
			event = $.event.fix(orgEvent);
			event.type = 'mousewheel';

			// Old school scrollwheel delta
			if ( 'detail'      in orgEvent ) { deltaY = orgEvent.detail * -1;      }
			if ( 'wheelDelta'  in orgEvent ) { deltaY = orgEvent.wheelDelta;       }
			if ( 'wheelDeltaY' in orgEvent ) { deltaY = orgEvent.wheelDeltaY;      }
			if ( 'wheelDeltaX' in orgEvent ) { deltaX = orgEvent.wheelDeltaX * -1; }

			// Firefox < 17 horizontal scrolling related to DOMMouseScroll event
			if ( 'axis' in orgEvent && orgEvent.axis === orgEvent.HORIZONTAL_AXIS ) {
				deltaX = deltaY * -1;
				deltaY = 0;
			}

			// Set delta to be deltaY or deltaX if deltaY is 0 for backwards compatabilitiy
			delta = deltaY === 0 ? deltaX : deltaY;

			// New school wheel delta (wheel event)
			if ( 'deltaY' in orgEvent ) {
				deltaY = orgEvent.deltaY * -1;
				delta  = deltaY;
			}
			if ( 'deltaX' in orgEvent ) {
				deltaX = orgEvent.deltaX;
				if ( deltaY === 0 ) { delta  = deltaX * -1; }
			}

			// No change actually happened, no reason to go any further
			if ( deltaY === 0 && deltaX === 0 ) { return; }

			// Need to convert lines and pages to pixels if we aren't already in pixels
			// There are three delta modes:
			//   * deltaMode 0 is by pixels, nothing to do
			//   * deltaMode 1 is by lines
			//   * deltaMode 2 is by pages
			if ( orgEvent.deltaMode === 1 ) {
				var lineHeight = $.data(this, 'mousewheel-line-height');
				delta  *= lineHeight;
				deltaY *= lineHeight;
				deltaX *= lineHeight;
			} else if ( orgEvent.deltaMode === 2 ) {
				var pageHeight = $.data(this, 'mousewheel-page-height');
				delta  *= pageHeight;
				deltaY *= pageHeight;
				deltaX *= pageHeight;
			}

			// Store lowest absolute delta to normalize the delta values
			absDelta = Math.max( Math.abs(deltaY), Math.abs(deltaX) );

			if ( !lowestDelta || absDelta < lowestDelta ) {
				lowestDelta = absDelta;

				// Adjust older deltas if necessary
				if ( shouldAdjustOldDeltas(orgEvent, absDelta) ) {
					lowestDelta /= 40;
				}
			}

			// Adjust older deltas if necessary
			if ( shouldAdjustOldDeltas(orgEvent, absDelta) ) {
				// Divide all the things by 40!
				delta  /= 40;
				deltaX /= 40;
				deltaY /= 40;
			}

			// Get a whole, normalized value for the deltas
			delta  = Math[ delta  >= 1 ? 'floor' : 'ceil' ](delta  / lowestDelta);
			deltaX = Math[ deltaX >= 1 ? 'floor' : 'ceil' ](deltaX / lowestDelta);
			deltaY = Math[ deltaY >= 1 ? 'floor' : 'ceil' ](deltaY / lowestDelta);

			// Add information to the event object
			event.deltaX = deltaX;
			event.deltaY = deltaY;
			event.deltaFactor = lowestDelta;
			// Go ahead and set deltaMode to 0 since we converted to pixels
			// Although this is a little odd since we overwrite the deltaX/Y
			// properties with normalized deltas.
			event.deltaMode = 0;

			// Add event and delta to the front of the arguments
			args.unshift(event, delta, deltaX, deltaY);

			// Clearout lowestDelta after sometime to better
			// handle multiple device types that give different
			// a different lowestDelta
			// Ex: trackpad = 3 and mouse wheel = 120
			if (nullLowestDeltaTimeout) { clearTimeout(nullLowestDeltaTimeout); }
			nullLowestDeltaTimeout = setTimeout(nullLowestDelta, 200);

			return ($.event.dispatch || $.event.handle).apply(this, args);
		}

		function nullLowestDelta() {
			lowestDelta = null;
		}

		function shouldAdjustOldDeltas(orgEvent, absDelta) {
			// If this is an older event and the delta is divisable by 120,
			// then we are assuming that the browser is treating this as an
			// older mouse wheel event and that we should divide the deltas
			// by 40 to try and get a more usable deltaFactor.
			// Side note, this actually impacts the reported scroll distance
			// in older browsers and can cause scrolling to be slower than native.
			// Turn this off by setting $.event.special.mousewheel.settings.adjustOldDeltas to false.
			return special.settings.adjustOldDeltas && orgEvent.type === 'mousewheel' && absDelta % 120 === 0;
		}

	})();

	////////////// ON DOCUMENT READY /////////////////
	$(doc).ready(prepareDOM);

	// Expose:
	window._photobox = {
		history : history,
		defaults : defaults
	};
})(jQuery, document, window);