$(function () {
    var fileBrowser = "#file-browser";
    $(fileBrowser)
        .jstree({
            // List of active plugins
            "plugins": [
                "themes", "json_data", "ui", "crrm", "cookies", "dnd", "search", "types", "hotkeys", "contextmenu"
            ],
            "themes": {
                "theme": "default",
                "dots": true,
                "icons": true,
                "url": "/Content/Media/themes/default/style.css"
            },
            // I usually configure the plugin that handles the data first
            // This example uses JSON as it is most common
            "json_data": {
                // This tree is ajax enabled - as this is most common, and maybe a bit more complex
                // All the options are almost the same as jQuery's AJAX (read the docs)
                "ajax": {
                    "url": "/Admin/Media/GetTreeData",
                    "type": "POST",
                    "dataType": "json",
                    "data": function(n) {
                        // the result is fed to the AJAX request `data` option
                        return {
                            "dir": n.attr ? n.attr("id") : ""
                        };
                    }
                }
            },
            // Configuring the search plugin
            "search": {
                // As this has been a common question - async search
                // Same as above - the `ajax` config option is actually jQuery's AJAX object
                "ajax": {
                    "url": "./server.php",
                    // You get the search string as a parameter
                    "data": function(str) {
                        return {
                            "operation": "search",
                            "search_str": str
                        };
                    }
                }
            },
            // Using types - most of the time this is an overkill
            // read the docs carefully to decide whether you need types
            "types": {
                // I set both options to -2, as I do not need depth and children count checking
                // Those two checks may slow jstree a lot, so use only when needed
                "max_depth": -2,
                "max_children": -2,
                // I want only `home` nodes to be root nodes 
                // This will prevent moving or creating any other type as a root node
                "valid_children": ["home"],
                "types": {
                    // The default type
                    "default": {
                        "icon": {
                            "image": "/Content/Media/themes/default/file.png"
                        }
                    },
                    "file": {
                        "icon": {
                            "image": "/Content/Media/themes/default/file.png"
                        }
                    },
                    // The `folder` type
                    "folder": {
                        // can have files and other folders inside of it, but NOT `home` nodes
                        "valid_children": ["file", "folder", "image", ".dll"],
                        "icon": {
                            "image": "/Content/Media/themes/default/folder.png"
                        }
                    },
                    // The `home` nodes 
                    "home": {
                        // can have files and folders inside, but NOT other `home` nodes    
                        "valid_children": ["default", "folder"],
                        "icon": {
                            "image": "/Content/Media/themes/default/home.png"
                        },
                        // those prevent the functions with the same name to be used on `home` nodes
                        // internally the `before` event is used
                        "start_drag": false,
                        "move_node": false,
                        "delete_node": false,
                        "remove": false
                    },
                    ".dll": {
                        "icon": {
                            "image": "/Content/Media/themes/default/dll.png"
                        }
                    },
                    "image": {
                        "icon": {
                            "image": "/Content/Media/themes/default/image.png"
                        }
                    },
                }
            }
        })
        .bind("open_node.jstree", function(event, data) {
            $.jstree._reference(this).select_node(this);
        })
        .bind("close_node.jstree", function(event, data) {
            //alert("close");
            $.jstree._reference(this).select_node(this);
        })
        .bind("click.jstree", function(e, data) {
            var id = $('#file-browser').jstree('get_selected').attr('id');
            var rel = $('#file-browser').jstree('get_selected').attr('rel');
            if (rel == 'image') {
                $("#filepickerpreview").attr("src", id);
            } else if (rel == 'folder') {
                $("#filepickerpreview").attr("src", "/Content/Media/themes/default/folder.png");
            } else {
                $("#filepickerpreview").attr("src", "/Content/Media/themes/default/file.png");
            }
        })
        .bind("create.jstree", function(e, data) {
            $.post(
                "/Admin/Media/CreateFolder",
                {
                    "path": data.rslt.parent.attr("id"),
                    "folder": data.rslt.name
                },
                function(r) {
                    if (r.status) {
                        $(data.rslt.obj).attr("id", r.path);
                        $(data.rslt.obj).attr("rel", "folder");
                    } else {
                        $.jstree.rollback(data.rlbk);
                    }
                }
            );
        })
        .bind("remove.jstree", function(e, data) {
            data.rslt.obj.each(function() {
                $.ajax({
                    async: false,
                    type: 'POST',
                    url: "/Admin/Media/Delete",
                    data: {
                        "path": this.id
                    },
                    success: function(r) {
                        if (!r.status) {
                            data.inst.refresh();
                        }
                    }
                });
            });
        })
        .bind("rename.jstree", function(e, data) {
            $.post(
                "/Admin/Media/Rename",
                {
                    "path": data.rslt.obj.attr("id"),
                    "name": data.rslt.new_name
                },
                function(r) {
                    if (r.status != 1) {
                        $.jstree.rollback(data.rlbk);
                        switch (r.status) {
                        case 3:
                            alert("The name of file/folder has already existed. Please rename and try again.");
                            break;
                        default:
                            alert("Error while rename file/folder. Please try again.");
                            break;
                        }
                    } else {
                        var currentNode = data.args[0];
                        currentNode.attr("id", r.path);
                        if (r.isFolder) {
                            alert("is folder");
                            data.inst.refresh(data.rslt.obj);
                        }
                    }
                }
            );
        })
        .bind("move_node.jstree", function(e, data) {
            data.rslt.o.each(function(i) {
                $.ajax({
                    async: false,
                    type: 'POST',
                    url: "/Admin/Media/MoveData",
                    data: {
                        "path": $(this).attr("id"),
                        "destination": data.rslt.np.attr("id"),
                        "copy": data.rslt.cy ? true : false
                    },
                    success: function(r) {
                        if (r.status != 1) {
                            switch (r.status) {
                            case 3:
                                alert("Cannot move parent folder to child folder. Please try again.");
                                break;
                            case 5:
                                alert("Cannot move file/folder to current folder. Please try again.");
                                break;
                            default:
                                alert("Error while moving file/folder. Please refresh and try again.");
                                break;
                            }
                            $.jstree.rollback(data.rlbk);
                        } else {
                            data.inst.refresh(data.inst._get_parent(data.rslt.oc));
                        }
                    }
                });
            });
        })
        .bind("before.jstree", function(e, data) {
            if (data.func === "remove" && !confirm("Are you sure you want to delete?")) {
                e.stopImmediatePropagation();
                return false;
            }
        })
        .delegate(".jstree-open>a", "click.jstree", function(event) {
            $.jstree._reference(this).close_node(this, false, false);
        })
        .delegate(".jstree-closed>a", "click.jstree", function(event) {
            $.jstree._reference(this).open_node(this, false, false);
        });
});