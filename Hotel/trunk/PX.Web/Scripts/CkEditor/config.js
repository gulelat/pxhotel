/**
 * @license Copyright (c) 2003-2014; CKSource - Frederico Knabben. All rights reserved.
 * For licensing; see LICENSE.html or http=//ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example=
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = '/Admin/Media/MediaBrowser';
    //config.filebrowserUploadUrl = '/Admin/FileManager/Browser/2';
    config.filebrowserImageBrowseUrl = '/Admin/Media/MediaBrowser';
    //config.filebrowserImageUploadUrl = '/Admin/FileManager/Upload';
    config.filebrowserWindowWidth = 1200;
    config.filebrowserWindowHeight = 640; 
    config.contentsCss = '/Content/FrontEnd/css/styles.css';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.allowedContent = true;
};
