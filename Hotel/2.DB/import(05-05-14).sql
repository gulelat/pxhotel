USE [khachsanpx_db]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [BirthDay], [Gender], [About], [AvatarFileName], [Address], [Status], [LastLogin], [Facebook], [Twitter], [Google], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'levanvunam@gmail.com', N'admin', N'Nam', N'Le', N'(098) 988-7332', N'273390999', CAST(0x0000A30800000000 AS DateTime), NULL, N'123123 12 312 12 3123 12 1 221 3123 123', N'2_20140426025651.jpg', N'Hoàng Hoa Thám, Ho Chi Minh City, Vietnam', 1, CAST(0x0000A31C01876C79 AS DateTime), N'aaa', N'sss', NULL, 0, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', CAST(0x0000A31C01876C79 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [BirthDay], [Gender], [About], [AvatarFileName], [Address], [Status], [LastLogin], [Facebook], [Twitter], [Google], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'iamnguyenhuykha@gmail.com', N'1234', N'Kha', N'Nguyen', N'(123) 123-2131', N'123', NULL, NULL, NULL, NULL, NULL, 1, CAST(0x0000A31C013BAD88 AS DateTime), NULL, NULL, NULL, 0, 1, CAST(0x0000A2F700315FCF AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013BAD88 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [BirthDay], [Gender], [About], [AvatarFileName], [Address], [Status], [LastLogin], [Facebook], [Twitter], [Google], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'liemnguyendl@gmail.com', N'123@abc', N'Liêm', N'Nguyễn', N'0987777980', N'1234567890', CAST(0x00007C8F00000000 AS DateTime), NULL, N'Nothing ... 
Update later ...', N'4_20140327030052.jpg', N'83/4 Ngô Quyền, tp. Đà Lạt, Lâm Đồng, Việt Nam', 1, CAST(0x0000A2FB00EF4F8E AS DateTime), NULL, NULL, NULL, 0, 1, CAST(0x0000A2FB00EE2E6A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FB00F76EC4 AS DateTime), N'')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGroups] ON 

GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Administrator', N'Administrator', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', CAST(0x0000A31901117EB7 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Moderator', N'Moderator', 2, 1, CAST(0x0000A2F30188E332 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Customer', N'Customer', 3, 1, CAST(0x0000A2F30188FB75 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[UserInGroups] ON 

GO
INSERT [dbo].[UserInGroups] ([Id], [UserId], [UserGroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, 2, 1, 0, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'system', NULL, NULL)
GO
INSERT [dbo].[UserInGroups] ([Id], [UserId], [UserGroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, 3, 1, 0, 1, CAST(0x0000A319002BD595 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[UserInGroups] ([Id], [UserId], [UserGroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, 4, 1, 0, 1, CAST(0x0000A31C0128604A AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserInGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Templates] ON 

GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Default.Testimonials', N'<div class="textHeadingDivFeedback col-lg-12">
  NHẬN XÉT
</div>
<div id="carouselTestimonial" class="owl-carousel">
@foreach(var item in Model){
	<div class="item">
    <article>
      <div class="contentTestimonial">
        <p>@item.Content</p>
        <div class="col-sm-4">
          <img src="@item.AvatarPath" alt="@item.Author" title="@item.Author" class="img-responsive" />
        </div>
        <div class="clearfix visible-xs"></div>
        <div class="col-sm-8">
          <div><a href="">@item.Author</a></div>
          @item.AuthorDescription
        </div>
      </div>
    </article>
  </div>
}
</div>
  ', N' System.Collections.Generic.List`1[[PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ', 1, 0, 1, CAST(0x0000A2FD01539B92 AS DateTime), N'system', CAST(0x0000A31901306A8E AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="/@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="/@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        <a href="/@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A30500373FB4 AS DateTime), N'system', CAST(0x0000A31B013D9D95 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="/@item.Url" tabindex="-1">@item.Name </a>
  @if(item.ChildMenus.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A30500F71C50 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31B013CDBD5 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'Default.GetPageContentTemplate', N'', N'PX.Business.Models.Pages.PageRenderModel', 1, 0, 1, CAST(0x0000A315000EEA01 AS DateTime), N'system', NULL, NULL)
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    @if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="/@item.Url">@item.Name</a>
  </li>
  }
}', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A31500F7BB39 AS DateTime), N'system', CAST(0x0000A31B013CE80F AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, N'Default.RotatingImagesTemplate', N'<div id="bigCarousel" class="owl-carousel">
@foreach(var item in Model.GalleryItems){
  <div class="item">
    <a href="@item.Url">
      <img class="lazyOwl" data-src="@item.ImageUrl" alt="">
    </a>
  </div>
}
</div>', N'PX.Business.Models.RotatingImageGroups.GroupGalleryModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 1, 0, 1, CAST(0x0000A3190126AC56 AS DateTime), N'system', CAST(0x0000A319012B6261 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, N'Default.Services', N'<div class="carouselServiceList owl-carousel">
@foreach(var item in Model){
  <div class="item">
    <div class="serviceList">
      <div class="serviceImg col-sm-6">
        <img src="@item.ImageUrl" class="img-thumbnail img-responsive"/>
      </div>
      <div class="serviceDescription col-sm-6">
        <div>@item.Description</div>
        <a href="@item.DetailsUrl" class="btn btn-default">Đọc thêm</a>
      </div>
    </div>
  </div>
}
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.Services.CurlyBrackets.ServiceCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A31A00F89CC9 AS DateTime), N'system', CAST(0x0000A31A0100846B AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, N'Default.News', N'<ul class="listNews">
@foreach(var item in Model){
  <li class="listNewsItem">
    <div class="newsItemDate col-sm-3 col-xs-3">@item.LastUpdate.Day
      <div>@item.LastUpdate.ToString("MMM")</div>
    </div>
    <div class="clearfix visible-xs"></div>
    <div class="newsItemContent col-sm-9">
      <div>@item.Description...<a href="@item.DetailsUrl">Xem thêm</a>
      </div>
    </li>
}
</ul>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A31A00FD6762 AS DateTime), N'system', CAST(0x0000A31A010067BF AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (12, N'Default.SingleBanner', N'<div class="adsOrPromo animated fadeInDown showDiv">
  <div class="adsTitle col-sm-8 col-sm-offset-1">
  	ƯU ĐÃI ĐẶC BIỆT
    <span>CHO KHÁCH HÀNG VIP DIAMOND</span>
  </div>
  <div class="clearfix"></div>
  <div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nhân dịp kỷ niệm 20 năm thành lập, trân trọng gửi đến quý khách hàng lâu năm thẻ VIP Diamond chương trình đặc biệt. Với mỗi hóa đơn từ 10.000.000 VNĐ, quý khách hàng sẽ được ưu đãi 50% giá trị. Dấu mốc đáng nhớ với PXHotel và quý vị! </div>
  <div class="clearfix"></div>
</div>', N'PX.Business.Models.Banners.CurlyBrackets.BannerCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 1, 0, 1, CAST(0x0000A31A0150C926 AS DateTime), N'system', CAST(0x0000A31A0151D8CB AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (13, N'Default.NewsListing', N'<ul class="blog-list">
    @foreach(var item in Model.NewsListing){
        <li>
            <div class="info">
                <div class="date">
                    <h4>@item.LastUpdate.Day</h4>
                    <span>@item.LastUpdate.ToString("MMM")</span>
                </div>
            </div>
            <div class="preview">
                <img class="img-responsive" src="@item.ImageUrl" />
                <a href="#">
                    <h3 class="blog-title">@item.Title</h3>
                </a>
                <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">@item.LastUpdatedBy</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    @foreach(var category in item.Categories){
                        <a href="@category.DetailsUrl">@category.Name ,</a> 
        			}
                    <span>|</span>
                </div>
                <div class="short-description">@item.Description</div>
                <a href="#">
                    <a href="@item.DetailsUrl" class="blog-readmore-button">Đọc thêm <i class="fa fa-eye"></i>
                    </a>
                </a>
                <div class="blog-comments">
                    <i class="fa fa-comments"></i>Bình luận: 16
                </div>
            </div>
        </li>
}
</ul>
<div class="blog-page-navigation-wrap">
    <ul>
        <li class="prev">
            <a href="#"><span class="fa fa-angle-left"></span>Prev                        
            </a>
        </li>
        <li class="current">
            <a href="#">1</a>
        </li>
        <li>
            <a href="#">2</a>
        </li>
        <li>
            <a href="#">3</a>
        </li>
        <li class="next">
            <a href="#">Next  
        <span class="fa fa-angle-right"></span>
            </a>
        </li>
    </ul>
</div>
<div class="clearfix"></div>
', N'PX.Business.Models.News.CurlyBrackets.NewsListingModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 1, 0, 1, CAST(0x0000A31A015F6C2E AS DateTime), N'system', CAST(0x0000A31C01713275 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (14, N'Default.CategoryListing', N'<div class="widget widget_category">
  <h4 class="title">Phân loại</h4>
  <div class="table-responsive">
    <table class="table table-hovered">
      @foreach(var item in Model.Categories){
      <tr>
        <td><i class="fa fa-angle-right"></i><a href="@item.DetailsUrl">@item.Name</a><span class="cateCount">(@item.Total)</span></td>
      </tr>
      }
    </table>
  </div>
</div>', N'PX.Business.Models.NewsCategories.CurlyBrackets.CategoriesModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 1, 0, 1, CAST(0x0000A31C016CA981 AS DateTime), N'system', CAST(0x0000A31C01711F09 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Templates] ([Id], [Name], [Content], [DataType], [IsDefaultTemplate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (15, N'Default.HotNews', N'<div class="widget latest_news">
  <h4 class="title">Tin nổi bật</h4>
  <ul class="list-news">
  @foreach(var item in Model){
  	<li>
      <img class="img-responsive img-thumbnail" style="width: 60px" alt="" src="@item.ImageUrl">
      <div class="text">
        <h5>
          <a href="@item.DetailsUrl">@item.Title</a>
        </h5>
        @item.Description
      </div>
    </li>
  }
  </ul>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 1, 0, 1, CAST(0x0000A31C016CA9B5 AS DateTime), N'system', CAST(0x0000A31C016EAEA6 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Templates] OFF
GO
SET IDENTITY_INSERT [dbo].[TemplateLogs] ON 

GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author

}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Create Template **', 0, 1, CAST(0x0000A3170130CC58 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author
12312
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A3170130EC63 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author
123121
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900B27A84 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  @Raw(item.ChildMenusString)
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Create Template **', 0, 1, CAST(0x0000A31900D0712E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, 5, N'Default.DynamicMenus', N'<div class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li class="dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>
', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Create Template **', 0, 1, CAST(0x0000A31900D07417 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  if(item.ChildItems.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900D2B7C5 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    if(item.ChildItems.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Create Template **', 0, 1, CAST(0x0000A31900D2C8FD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  @if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    @if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900D2CF52 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  @if(item.ChildItems.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900D2D1BF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  @if(item.ChildMenus.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900DB4F6C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  @if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900DBADAC AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (12, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  if(item.ChildMenus.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900DBDB3E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (13, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900E35ED8 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (14, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  @if(item.ChildMenus.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[PX.Business.Models.Pages.CurlyBrackets.DynamicMenuCurlyBracket]', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31900E42A08 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (15, 5, N'Default.DynamicMenus', N'<div class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li class="@(item.Active ? "active" : "") dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>
', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A3190104ACC7 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (16, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    @if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31901058E8E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (17, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author2
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null ', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31901097663 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (18, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author222
}', N'PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null ', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A3190109887B AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (19, 4, N'Default.Testimonials', N'@foreach(var item in Model){
	@item.Author
}', N' System.Collections.Generic.List`1[[PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A319010A3702 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (20, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>
<script type="text/javascript">
  var activePageId = activePageId || 0;
  $("#dynamicMenus li").removeClass("active");
  $("#dynamicMenus li[data-id=" + activePageId + "]").addClass("active");
</script>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A319011FC8FD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (21, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>
<script type="text/javascript">
  $(function(){
    var activePageId = activePageId || 0;
    alert(activePageId);
    $("#dynamicMenus li").removeClass("active");
    $("#dynamicMenus li[data-id=" + activePageId + "]").addClass("active");
  });
</script>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A3190120A664 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (22, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31901215A3C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (23, 9, N'Default.RotatingImagesTemplate', N'<div id="bigCarousel" class="owl-carousel">
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom1.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom2.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom3.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom4.jpg" alt="">
    </a>
  </div>
</div>', N'PX.Business.Models.Pages.PageRenderModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Create Template **', 0, 1, CAST(0x0000A3190126FE8C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (24, 9, N'Default.RotatingImagesTemplate', N'<div id="bigCarousel" class="owl-carousel">
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom1.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom2.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom3.jpg" alt="">
    </a>
  </div>
  <div class="item">
    <a href="#">
      <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom4.jpg" alt="">
    </a>
  </div>
</div>', N'PX.Business.Models.RotatingImageGroups.GroupGalleryModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A319012B629D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (25, 4, N'Default.Testimonials', N'<div id="carouselTestimonial" class="owl-carousel">
@foreach(var item in Model){
	<div class="item">
    <article>
      <div class="contentTestimonial">
        <p>@item.Content</p>
        <div class="col-sm-4">
          <img src="@item.AvatarPath" alt="@item.Author" title="@item.Author" class="img-responsive" />
        </div>
        <div class="clearfix visible-xs"></div>
        <div class="col-sm-8">
          <div><a href="">@item.Author</a></div>
          @item.AuthorDescription
        </div>
      </div>
    </article>
  </div>
}
</div>
  ', N' System.Collections.Generic.List`1[[PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A319012FECDE AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (26, 4, N'Default.Testimonials', N'<div id="carouselTestimonial" class="owl-carousel">
@foreach(var item in Model){
	<div class="item">
    <article>
      <div class="contentTestimonial">
        <p>@item.Content</p>
        <div class="col-sm-4">
          <img src="@item.AvatarPath" alt="@item.Author" title="@item.Author" class="img-responsive" />
        </div>
        <div class="clearfix visible-xs"></div>
        <div class="col-sm-8">
          <div><a href="">@item.Author</a></div>
          @item.AuthorDescription
        </div>
      </div>
    </article>
  </div>
}
</div>
  ', N' System.Collections.Generic.List`1[[PX.Business.Models.Testimonials.CurlyBrackets.TestimonialCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ', N'** Update Page Template **
- Update field: Content
', 0, 1, CAST(0x0000A31901306A97 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (27, 10, N'Default.Services', N'<div class="carouselServiceList owl-carousel">
@foreach(var item in Model){
  <div class="item">
    <div class="serviceList">
      <div class="serviceImg col-sm-6">
        <img src="@item.ImageUrl" class="img-thumbnail img-responsive"/>
      </div>
      <div class="serviceDescription col-sm-6">
        <div>@item.Description</div>
        <a href="@item.DetailUrl" class="btn btn-default">Đọc thêm</a>
      </div>
    </div>
  </div>
}
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.Services.CurlyBrackets.ServiceCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Create Template **', 0, 1, CAST(0x0000A31A00FA1AA4 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (28, 11, N'Default.News', N'<ul class="listNews">
@foreach(var item in Model){
  <li class="listNewsItem">
    <div class="newsItemDate col-sm-3 col-xs-3">@item.LastUpdate.Day
      <div>@item.LastUpdate.ToString("MMM")</div>
    </div>
    <div class="clearfix visible-xs"></div>
    <div class="newsItemContent col-sm-9">
      <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
      </div>
    </li>
}
</ul>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Create Template **', 0, 1, CAST(0x0000A31A00FF2655 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (29, 11, N'Default.News', N'<ul class="listNews">
@foreach(var item in Model){
  <li class="listNewsItem">
    <div class="newsItemDate col-sm-3 col-xs-3">@item.LastUpdate.Day
      <div>@item.LastUpdate.ToString("MMM")</div>
    </div>
    <div class="clearfix visible-xs"></div>
    <div class="newsItemContent col-sm-9">
      <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
      </div>
    </li>
}
</ul>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31A0100683C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (30, 10, N'Default.Services', N'<div class="carouselServiceList owl-carousel">
@foreach(var item in Model){
  <div class="item">
    <div class="serviceList">
      <div class="serviceImg col-sm-6">
        <img src="@item.ImageUrl" class="img-thumbnail img-responsive"/>
      </div>
      <div class="serviceDescription col-sm-6">
        <div>@item.Description</div>
        <a href="@item.DetailUrl" class="btn btn-default">Đọc thêm</a>
      </div>
    </div>
  </div>
}
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.Services.CurlyBrackets.ServiceCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31A010084A4 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (31, 12, N'Default.SingleBanner', N'<div class="adsOrPromo animated fadeInDown showDiv">
<div class="adsTitle col-sm-8 col-sm-offset-1">
ƯU ĐÃI ĐẶC BIỆT
<span>CHO KHÁCH HÀNG VIP DIAMOND</span>
</div>
<div class="clearfix"></div>
<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nhân dịp kỷ niệm 20 năm thành lập, trân trọng gửi đến quý khách hàng lâu năm thẻ VIP Diamond chương trình đặc biệt. Với mỗi hóa đơn từ 10.000.000 VNĐ, quý khách hàng sẽ được ưu đãi 50% giá trị. Dấu mốc đáng nhớ với PXHotel và quý vị! </div>
<div class="clearfix"></div>
</div>', N'PX.Business.Models.Banners.CurlyBrackets.BannerCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Create Template **', 0, 1, CAST(0x0000A31A0151391E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (32, 12, N'Default.SingleBanner', N'<div class="adsOrPromo animated fadeInDown showDiv">
<div class="adsTitle col-sm-8 col-sm-offset-1">
ƯU ĐÃI ĐẶC BIỆT
<span>CHO KHÁCH HÀNG VIP DIAMOND</span>
</div>
<div class="clearfix"></div>
<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nhân dịp kỷ niệm 20 năm thành lập, trân trọng gửi đến quý khách hàng lâu năm thẻ VIP Diamond chương trình đặc biệt. Với mỗi hóa đơn từ 10.000.000 VNĐ, quý khách hàng sẽ được ưu đãi 50% giá trị. Dấu mốc đáng nhớ với PXHotel và quý vị! </div>
<div class="clearfix"></div>
</div>', N'PX.Business.Models.Banners.CurlyBrackets.BannerCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31A0151D900 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (33, 13, N'Default.NewsListing', N'<ul class="blog-list">
    @foreach(var item in Model.NewsListing){
        <li>
            <div class="info">
                <div class="date">
                    <h4>@item.LastUpdate.Day</h4>
                    <span>@item.LastUpdate.ToString("MMM")</span>
                </div>
            </div>
            <div class="preview">
                <img class="img-responsive" src="@item.ImageUrl" />
                <a href="#">
                    <h3 class="blog-title">@item.Title</h3>
                </a>
                <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">@item.LastUpdatedBy</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    @foreach(var category in item.Categories){
                        <a href="#">@category.Name ,</a> 
        }
                    <span>|</span>
                </div>
                <div class="short-description">@item.Description</div>
                <a href="#">
                    <a href="@item.DetailsUrl" class="blog-readmore-button">Đọc thêm <i class="fa fa-eye"></i>
                    </a>
                </a>
                <div class="blog-comments">
                    <i class="fa fa-comments"></i>Bình luận: 16
                </div>
            </div>
        </li>
}
</ul>
<div class="blog-page-navigation-wrap">
    <ul>
        <li class="prev">
            <a href="#"><span class="fa fa-angle-left"></span>Prev                        
            </a>
        </li>
        <li class="current">
            <a href="#">1</a>
        </li>
        <li>
            <a href="#">2</a>
        </li>
        <li>
            <a href="#">3</a>
        </li>
        <li class="next">
            <a href="#">Next  
        <span class="fa fa-angle-right"></span>
            </a>
        </li>
    </ul>
</div>
<div class="clearfix"></div>
', N'PX.Business.Models.News.CurlyBrackets.NewsListingModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Create Template **', 0, 1, CAST(0x0000A31A0168F34A AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (34, 13, N'Default.NewsListing', N'<ul class="blog-list">
    @foreach(var item in Model.NewsListing){
        <li>
            <div class="info">
                <div class="date">
                    <h4>@item.LastUpdate.Day</h4>
                    <span>@item.LastUpdate.ToString("MMM")</span>
                </div>
            </div>
            <div class="preview">
                <img class="img-responsive" src="@item.ImageUrl" />
                <a href="#">
                    <h3 class="blog-title">@item.Title</h3>
                </a>
                <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">@item.LastUpdatedBy</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    @foreach(var category in item.Categories){
                        <a href="#">@category.Name ,</a> 
        			}
                    <span>|</span>
                </div>
                <div class="short-description">@item.Description</div>
                <a href="#">
                    <a href="@item.DetailsUrl" class="blog-readmore-button">Đọc thêm <i class="fa fa-eye"></i>
                    </a>
                </a>
                <div class="blog-comments">
                    <i class="fa fa-comments"></i>Bình luận: 16
                </div>
            </div>
        </li>
}
</ul>
<div class="blog-page-navigation-wrap">
    <ul>
        <li class="prev">
            <a href="#"><span class="fa fa-angle-left"></span>Prev                        
            </a>
        </li>
        <li class="current">
            <a href="#">1</a>
        </li>
        <li>
            <a href="#">2</a>
        </li>
        <li>
            <a href="#">3</a>
        </li>
        <li class="next">
            <a href="#">Next  
        <span class="fa fa-angle-right"></span>
            </a>
        </li>
    </ul>
</div>
<div class="clearfix"></div>
', N'PX.Business.Models.News.CurlyBrackets.NewsListingModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31A016D80BE AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (35, 6, N'Default.DynamicMenus.Children', N'@foreach(var item in Model){
<li class="dropdown-submenu">
  <a href="@item.Url" tabindex="-1">@item.Name </a>
  @if(item.ChildMenus.Any()){
  	<ul class="dropdown-menu">
  	@Raw(item.ChildMenusString)
  	</ul>
  }
</li>
}', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31B013CDBF2 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (36, 8, N'Default.DynamicMenus.Mobile', N'@foreach(var item in Model){
  if(item.ChildMenus.Any()){
  <li class="subMenuWithImage">
    <div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
    @if(item.ChildMenus.Any()){
      <ul class="dropdown-menu">
      @Raw(item.ChildMenusString)
      </ul>
    }
  </li>
  }
  else{
  <li class="visible-xs menuItemOnMobileWithoutChildren">
    <a href="@item.Url">@item.Name</a>
  </li>
  }
}', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31B013CE81A AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (37, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="/@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="/@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        	<a href="@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31B013D06B4 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (38, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="/@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="/@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        <a href="/@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31B013D3156 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (39, 5, N'Default.DynamicMenus', N'<div id="dynamicMenus" class="navbar navbar-static-top navbar-default" role="navigation">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>
  <div class="navbar-collapse collapse">
    <ul class="nav navbar-nav myNavbarNav">
      @foreach(var item in Model){
      <li id="@item.PageId" class="@item dropdown parentDropdownMenu hidden-xs">
        <a class="dropdown-submenu" data-toggle="dropdown" href="/@item.Url">@item.Name</a>
        @if(item.ChildMenus.Any()){
        <ul class="dropdown-menu">
          @Raw(item.ChildMenusString)
        </ul>
        }
      </li>
      <li class="visible-xs @(item.ChildMenus.Any() ? "menuItemOnMobile" : "menuItemOnMobileWithoutChildren")">
        @if(item.ChildMenus.Any()){
        	<div class="linkItemLevel@(item.Level)OnMobile">@item.Name</div>
        	<ul class="menuItemContainerLevel@(item.Level)OnMobile">
              <li class="visible-xs menuItemOnMobileWithoutChildren">
                <a href="/@item.Url">OverView</a>
              </li>
              @Raw(item.ChildMenusMobileString)
        	</ul>
        }
        else{
        <a href="/@item.Url">@item.Name</a>
        }
      </li>
      }
    </ul>
  </div>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.ClientMenus.CurlyBrackets.DynamicMenuCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31B013D9D9D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (40, 15, N'Default.HotNews', N'<div class="widget latest_news">
  <h4 class="title">Tin mới nhất</h4>
  <ul class="list-news">
  @foreach(var item in Model){
  	<li>
      <img class="img-responsive img-thumbnail" alt="" src="@item.ImageUrl">
      <div class="text">
        <h5>
          <a href="@item.DetailsUrl">@item.Title</a>
        </h5>
        @item.Description
      </div>
    </li>
  }
  </ul>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Create Template **', 0, 1, CAST(0x0000A31C016DF052 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (41, 15, N'Default.HotNews', N'<div class="widget latest_news">
  <h4 class="title">Tin nổi bật</h4>
  <ul class="list-news">
  @foreach(var item in Model){
  	<li>
      <img class="img-responsive img-thumbnail" alt="" src="@item.ImageUrl">
      <div class="text">
        <h5>
          <a href="@item.DetailsUrl">@item.Title</a>
        </h5>
        @item.Description
      </div>
    </li>
  }
  </ul>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31C016E2A23 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (42, 15, N'Default.HotNews', N'<div class="widget latest_news">
  <h4 class="title">Tin nổi bật</h4>
  <ul class="list-news">
  @foreach(var item in Model){
  	<li>
      <img class="img-responsive img-thumbnail" alt="" src="@item.ImageUrl">
      <div class="text">
        <h5>
          <a href="@item.DetailsUrl">@item.Title</a>
        </h5>
        @item.Description
      </div>
    </li>
  }
  </ul>
</div>', N'System.Collections.Generic.List`1[[PX.Business.Models.News.CurlyBrackets.NewsCurlyBracket, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31C016EAEDD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (43, 14, N'Default.CategoryListing', N'', N'PX.Business.Models.NewsCategories.CurlyBrackets.CategoriesModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Create Template **', 0, 1, CAST(0x0000A31C01711F7A AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[TemplateLogs] ([Id], [TemplateId], [Name], [Content], [DataType], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (44, 13, N'Default.NewsListing', N'<ul class="blog-list">
    @foreach(var item in Model.NewsListing){
        <li>
            <div class="info">
                <div class="date">
                    <h4>@item.LastUpdate.Day</h4>
                    <span>@item.LastUpdate.ToString("MMM")</span>
                </div>
            </div>
            <div class="preview">
                <img class="img-responsive" src="@item.ImageUrl" />
                <a href="#">
                    <h3 class="blog-title">@item.Title</h3>
                </a>
                <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">@item.LastUpdatedBy</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    @foreach(var category in item.Categories){
                        <a href="#">@category.Name ,</a> 
        			}
                    <span>|</span>
                </div>
                <div class="short-description">@item.Description</div>
                <a href="#">
                    <a href="@item.DetailsUrl" class="blog-readmore-button">Đọc thêm <i class="fa fa-eye"></i>
                    </a>
                </a>
                <div class="blog-comments">
                    <i class="fa fa-comments"></i>Bình luận: 16
                </div>
            </div>
        </li>
}
</ul>
<div class="blog-page-navigation-wrap">
    <ul>
        <li class="prev">
            <a href="#"><span class="fa fa-angle-left"></span>Prev                        
            </a>
        </li>
        <li class="current">
            <a href="#">1</a>
        </li>
        <li>
            <a href="#">2</a>
        </li>
        <li>
            <a href="#">3</a>
        </li>
        <li class="next">
            <a href="#">Next  
        <span class="fa fa-angle-right"></span>
            </a>
        </li>
    </ul>
</div>
<div class="clearfix"></div>
', N'PX.Business.Models.News.CurlyBrackets.NewsListingModel, PX.Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', N'** Update Template **
- Update field: Content
', 0, 1, CAST(0x0000A31C017132B9 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TemplateLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

GO
INSERT [dbo].[Tags] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Tag 1', 0, 1, CAST(0x0000A30D018498B8 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31900B07478 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Tags] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Tag 2', 0, 1, CAST(0x0000A30D01849FBA AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31900B08114 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Tags] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Tag 3', 0, 1, CAST(0x0000A30D0184A81C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[Tags] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Tag 4', 0, 1, CAST(0x0000A30D0184B2A1 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
SET IDENTITY_INSERT [dbo].[PageTemplates] ON 

GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1005, N'Master', N'<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="PXHotel, {Caption}">
    <meta name="author" content="PXHotel">
    <link rel="shortcut icon" href="favicon.png">

    <title>PXHotel - {Title}</title>

    <!-- Bootstrap core CSS -->
    <link href="~/Content/FrontEnd/css/bootstrap.css" rel="stylesheet">

    <!-- Just for debugging purposes. Don''t actually copy this line! -->
    <!--[if lt IE 9]><script src="~/Scripts/FrontEnd/ie8-responsive-file-warning.js"></script><![endif]-->

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.~/Scripts/FrontEnd/1.3.0/respond.min.js"></script>
    <![endif]-->

    <!-- Custom styles for this template -->
    <link href="~/Content/FrontEnd/css/styles.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/font-awesome.min.css" rel="stylesheet">
    <link href="fonts/allura-regular-webfont.css" rel="stylesheet">
    <link href="fonts/fontviet1-webfont.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/owl.carousel.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/owl.theme.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/owl.transitions.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/animate.min.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/datepicker.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/datepicker.date.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/photobox.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/photobox.ie.css" rel="stylesheet">
    <link href="~/Content/FrontEnd/css/summernote.css" rel="stylesheet">
    <script src="~/Scripts/FrontEnd/jquery-1.10.2.min.js"></script>
  </head>
  <body>
    <div class="bigWrapper col-lg-10 col-lg-offset-1">
      <div>
        <div class="divMenuAndLogo">
          {DynamicMenu}
          <div class="divLogo col-sm-2 col-sm-offset-5 col-xs-4 col-xs-offset-4">
              <img src="/Content/FrontEnd/img/hotelLogo.png" class="img-responsive" />
          </div>
          <div class="clearfix">
          </div>
          <div class="col-lg-4 col-lg-offset-4 divTextLuxuryHotel">
            <hr>
            <div>
              <div>Luxury Hotel</div>
            </div>
          </div>
          <div class="clearfix">
          </div>
          <div class="lineSeparator"><hr></div>
        </div>
        {RenderBody}
        <footer>
          <div class="col-sm-6">
            PX HOTEL &copy; 2014
          </div>
          <div class="col-sm-6">
            <ul class="containerSocialLinkAtFooter">
              <li>
                <a href="#" class="iconSocialAtFooter facebookIcon">&#61594;</a>
                <a href="#" class="iconSocialAtFooter googleIcon">&#61653;</a>
                <a href="#" class="iconSocialAtFooter youTubeIcon">&#61799;</a>
                <a href="#" class="iconSocialAtFooter twitterIcon">&#61593;</a>
                <a href="#" class="iconSocialAtFooter rssIcon">&#xF09E;</a>
              </li>
            </ul>
          </div>
          <div class="clearfix">
          </div>
        </footer>
        <div class="fixedNavigation">
          <ul class="smallItemWrapperFixedNavigation">
            <li class="smallItemFixedNavigation">
              <a id="btnScrollTop" href="" title="Top"><i class="fa fa-arrow-up"></i></a>
            </li>
          </ul>
        </div>
      </div>
    </div>

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="~/Scripts/FrontEnd/bootstrap.min.js"></script>
    <script src="~/Scripts/FrontEnd/owl.carousel.min.js"></script>
    <script src="~/Scripts/FrontEnd/waypoints.min.js"></script>
    <script src="~/Scripts/FrontEnd/imagesloaded.pkgd.min.js"></script>
    <script src="~/Scripts/FrontEnd/picker.js"></script>
    <script src="~/Scripts/FrontEnd/picker.date.js"></script>
    <script src="~/Scripts/FrontEnd/jquery.photobox.js"></script>
    <script src="~/Scripts/FrontEnd/summernote.min.js"></script>
    <script src="~/Scripts/FrontEnd/jquery.parallax-1.1.3.js"></script>
    <script type="text/javascript">
      $(document).ready(function(){
      	var activePageId = "{Id}";
        $("#dynamicMenus li").removeClass("active");
        $("#dynamicMenus li#" + activePageId + "").addClass("active");
        
        $("#bigCarousel").owlCarousel({
          autoPlay : 3000,
          stopOnHover : true,
          navigation:true,
          paginationSpeed : 1000,
          paginationNumbers: true,
          lazyLoad : true,
          goToFirstSpeed : 2000,
          singleItem : true,
          autoHeight : true,
          transitionStyle:"fade",
          navigationText: [
          "<i class=''fa fa-chevron-left''></i>",
          "<i class=''fa fa-chevron-right''></i>"]
        });

        $(".carouselServiceList").owlCarousel({
          autoPlay: 5000,
          stopOnHover: true,
          paginationNumbers: true,
          lazyLoad: true,
          singleItem : true
        });

        $("#carouselTestimonial").owlCarousel({
          autoPlay: 5000,
          singleItem: true,
          pagination: false,
          transitionStyle: "backSlide"
        });

        $(''#bigCarousel'').addClass(''animated bounceIn'');

        $(''.divWrapperTwoHalf'').imagesLoaded(function( instance ) {
          $(''.divWrapperTwoHalf'').waypoint(function(direction) {
            if (direction == "down"){
              $(''.divHalfContentWrapper.halfOnRight'').addClass(''animated fadeInRight showDiv'');
              $(''.divHalfContentWrapper.halfOnLeft'').addClass(''animated fadeInLeft showDiv'');
            }
          }, {
            offset: function() {
              if ($(window).width() < 768)
                return ($(window).height());
              else
                return ($(''#bigCarousel'').height()*1.5);
            }
          });
        });

        $(''.adsOrPromo'').imagesLoaded(function( instance ) {
          $(''.adsOrPromo'').waypoint(function(direction) {
            if (direction == "down"){
              $(this).addClass(''animated fadeInDown showDiv'');
            }
          }, {
            offset: function() {
              return ($(window).height() - $(this).height()*0.5);
            }
          });
        });

        $(''.divFeedbackAndGallery'').imagesLoaded(function( instance ) {
          $(''.divFeedbackAndGallery'').waypoint(function(direction) {
            if (direction == "down"){
              $(this).find(''.divWrapperTestimonial'').addClass(''animated bounceIn showDiv'');
              $(this).find(''.divWrapperFeedback'').addClass(''animated bounceIn showDiv'');
              $(this).find(''.divWrapperContact'').addClass(''animated bounceIn showDiv'');
              $(this).find(''.divWrapperImageGallery'').addClass(''animated bounceIn showDiv'');
            }
          }, {
            offset: function() {
              if ($(window).width() < 768)
                return ($(window).height());
              else
                return ($(window).height() - $(this).height()*0.5);
            }
          });
        });              

        /*Small Fixed Navigation behavior*/
        if ($(window).scrollTop() == 0)
          $("#btnScrollTop").css({"color":"#000000"});
          
        $(function () {
          $(window).scroll(function () {
            if ($(this).scrollTop() > 10) {
              $(''#btnScrollTop'').css({"color":"#FFFFFF"});
              $(''.fixedNavigation'').addClass(''move'');
            } else {
              $(''#btnScrollTop'').css({"color":"#000000"});
              $(''.fixedNavigation'').removeClass(''move'');
            }
          });
        });

        $(''#btnScrollTop'').click(function () {
          $(''body,html'').animate({
            scrollTop: 0
          }, 800);
          return false;
        });

        $(''.divSmallSquareImg > div'').hover(function(){
          $(this).addClass(''smallHovered'');
        },function(){
          $(this).removeClass(''smallHovered'');
        });

        // Datepicker
        $(''.datepickerfield'').pickadate({
          monthsFull: [''Tháng Một'', ''Tháng Hai'', ''Tháng Ba'', ''Tháng Tư'', ''Tháng Năm'', ''Tháng Sáu'', ''Tháng Bảy'', ''Tháng Tám'', ''Tháng Chín'', ''Tháng Mười'', ''Tháng Mười Một'', ''Tháng Mười Hai''],
          monthsShort: [''Th 1'', ''Th 2'', ''Th 3'', ''Th 4'', ''Th 5'', ''Th 6'', ''Th 7'', ''Th 8'', ''Th 9'', ''Th 10'', ''Th 11'', ''Th 12''],
          weekdaysFull: [''Chủ Nhật'', ''Thứ Hai'', ''Thứ Ba'', ''Thứ Tư'', ''Thứ Năm'', ''Thứ Sáu'', ''Thứ Bảy''],
          weekdaysShort: [''CN'', ''T2'', ''T3'', ''T4'', ''T5'', ''T6'', ''T7''],
          today: ''Hôm nay'',
          clear: ''Xóa'',
          format: ''dd/mm/yyyy''
        });

        // Photobox
        $(''#gallery'').photobox({
          time: 2000
        });

        // Hover the heading text
        $(''.divNewsEvents'').hover(function(){
          $(this).find(''.text-center'').addClass(''animated fadeInDown'');
        }, function(){
          $(this).find(''.text-center'').removeClass(''animated fadeInDown'');
        });

        $(''.divServices'').hover(function(){
          $(this).find(''.text-center'').addClass(''animated fadeInDown'');
        }, function(){
          $(this).find(''.text-center'').removeClass(''animated fadeInDown'');
        });

        $(''.divBooking'').hover(function(){
          $(this).find(''.text-center'').addClass(''animated fadeInDown'');
        }, function(){
          $(this).find(''.text-center'').removeClass(''animated fadeInDown'');
        });

        // Hover the heading text
        $(''.divWrapperTestimonial'').hover(function(){
          $(this).find(''.textHeadingDivFeedback'').addClass(''animated fadeInRight'');
        }, function(){
          $(this).find(''.textHeadingDivFeedback'').removeClass(''animated fadeInRight'');
        });

        $(''.divWrapperFeedback'').hover(function(){
          $(this).find(''.textHeadingDivFeedback'').addClass(''animated fadeInRight'');
        }, function(){
          $(this).find(''.textHeadingDivFeedback'').removeClass(''animated fadeInRight'');
        });

        $(''.divWrapperContact'').hover(function(){
          $(this).find(''.textHeadingDivFeedback'').addClass(''animated fadeInRight'');
        }, function(){
          $(this).find(''.textHeadingDivFeedback'').removeClass(''animated fadeInRight'');
        });

        $(''.divWrapperImageGallery'').hover(function(){
          $(this).find(''.textHeadingDivFeedback'').addClass(''animated fadeInRight'');
        }, function(){
          $(this).find(''.textHeadingDivFeedback'').removeClass(''animated fadeInRight'');
        });

        // Hover the menu
        $(''li.parentDropdownMenu'').hover(function(){
          $(this).find(''ul.dropdown-menu'').addClass(''animated fadeInUp MenuHovered'');
        }, function(){
          $(this).find(''ul.dropdown-menu'').removeClass(''animated fadeInUp MenuHovered'');
        });

        $(''li.dropdown-submenu'').hover(function(){
          $(this).find(''ul.dropdown-menu'').addClass(''animated fadeInRight MenuHovered'');
        }, function(){
          $(this).find(''ul.dropdown-menu'').removeClass(''animated fadeInRight MenuHovered'');
        });

        // IF MENU HAS SUBMENU(S), TURN ON RIGHT ARROW
        $(".myNavbarNav .dropdown .dropdown-menu .dropdown-submenu").each(function(index){
          if($(this).children("ul.dropdown-menu").length > 0){
            $(this).children("a").after().addClass("HasChildMenusArrow");
          }
        });

        // Enable click
        $(''.myNavbarNav > li a'').click(function(){
          window.location.href = $(this).attr("href");
        });

        // Menu on mobile
        $(''.linkItemLevel1OnMobile'').click(function() {
            //collapse another menu before expand this
            $.each($(this).parent().siblings(), function(i, val){
              if ($(this).find("ul").is(":visible")){
                $(this).find("ul")[0].style.display = "none";
                $(this).removeClass("AfterExpand");
              }
            });

            $(this).siblings(''ul.menuItemContainerLevel1OnMobile'').toggle();
            $(this).parent().toggleClass("AfterExpand");
            if ($(this).parent().hasClass(''AfterExpand'')){
              $(''html, body'').animate({
              scrollTop: $(this).offset().top - 13}, 350);  
            }
            else
              $(''html, body'').animate({
              scrollTop: 0}, 350);
            
        });

        $(''.linkItemLevel2OnMobile'').click(function() {
            //collapse another menu before expand this
            $.each($(this).parent().siblings(), function(i, val){
              if ($(this).find("ul").is(":visible")){
                $(this).find("ul")[0].style.display = "none";
                $(this).removeClass("AfterExpand");
              }
            });

            $(this).siblings(''ul.menuItemContainerLevel2OnMobile'').toggle();
            $(this).parent().toggleClass("AfterExpand");
            if ($(this).parent().hasClass(''AfterExpand'')){
              $(''html, body'').animate({
              scrollTop: $(this).offset().top - 13}, 350);  
            }
            else
              $(''html, body'').animate({
              scrollTop: 0}, 350);
            
        });
      });
    </script>
  </body>
</html>
', N'.01005.', NULL, 0, 1, CAST(0x0000A2FA00A524CB AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31B013571B1 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1007, N'News', N'{RenderBody}
<div class="col-sm-4">{HotNews_2} {CategoryListing}</div>
<div class="clearfix">&nbsp;</div>
</div>', N'.01005.01007.', 1005, 0, 1, CAST(0x0000A308017F6085 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C0185C5F5 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1008, N'Test 2', N'chủ {RenderBody}', N'.01008.', NULL, 0, 1, CAST(0x0000A3120170DC80 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31B00F0FA31 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1009, N'DefaultMasterTemplateWithRenderContentOnly', N'{RenderBody}', N'.01009.', NULL, 0, 1, CAST(0x0000A31300DB4A23 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31900C01FC5 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[PageTemplates] OFF
GO
SET IDENTITY_INSERT [dbo].[FileTemplates] ON 

GO
INSERT [dbo].[FileTemplates] ([Id], [Name], [Controller], [Action], [Parameters], [PageTemplateId], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Test 22', N'Home', N'Index', N'id=1', 1005, NULL, N'.00001.', 0, 1, CAST(0x0000A3100128207A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3140135165D AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[FileTemplates] OFF
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Trang Chủ', N'Home page', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<p>{Banner_1}</p>

<div class="clearfix">&nbsp;</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {News_2}</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="divFeedbackAndGallery">
<div class="col-lg-6">
<div class="col-lg-7 divWrapperTestimonial">{Testimonials}</div>

<div class="col-lg-5 divWrapperFeedback">
<div class="textHeadingDivFeedback col-lg-12">&Yacute; KIẾN</div>

<form>
<div class="form-group"><input type="text" /></div>

<div class="form-group"><textarea rows="3"></textarea></div>
Gửi phản hồi</form>
</div>
</div>

<div class="col-lg-3 divWrapperContact">
<div class="textHeadingDivFeedback col-lg-12">LI&Ecirc;N HỆ</div>

<ul>
	<li>
	<p><strong>Địa chỉ:</strong> 112 Phan Đ&igrave;nh Ph&ugrave;ng, Phường 6, TP. Đ&agrave; Lạt</p>
	</li>
	<li>
	<p><strong>Phone:</strong> +84 63 00000</p>
	</li>
	<li>
	<p><strong>Email:</strong><a href="mailto:pxmail@yahoo.com">pxmail@yahoo.com</a></p>
	</li>
</ul>
</div>

<div class="col-lg-3 divWrapperImageGallery">
<div class="textHeadingDivFeedback col-lg-12">THƯ VIỆN ẢNH</div>

<div id="gallery">
<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg1.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg1.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg2.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg2.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg3.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg3.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg4.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg4.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg5.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg5.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg6.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg6.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, 1, N'.00005.', NULL, 1, NULL, NULL, NULL, 38, 1, CAST(0x0000A2FA00A55DC9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C01744711 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'Dịch Vụ', N'Dịch vụ', N'123', N'<p>{Testimonials_5}</p>
', N'<p>{Page}</p>
', N'Dich-Vu', 1005, NULL, 1, 0, N'.00006.', NULL, 1, NULL, NULL, NULL, 39, 1, CAST(0x0000A2FD00E949B0 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A01617B5F AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
<span>News</span></div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>

<div class="col-sm-4">{HotNews_2} {CategoryListing}</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, 0, N'.00007.', NULL, 1, NULL, NULL, NULL, 40, 1, CAST(0x0000A305003123E3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C0186D1B7 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'Phòng Nghỉ', N'Phòng Nghỉ', NULL, N'<p>Ph&ograve;ng Nghỉ</p>
', NULL, N'Phong-Nghi', 1005, NULL, 1, 0, N'.00008.', NULL, 1, NULL, NULL, NULL, 41, 1, CAST(0x0000A30500313B67 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A0161AE00 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, N'Liên Hệ', N'Liên Hệ', NULL, N'<p>Li&ecirc;n Hệ</p>
', NULL, N'Lien-He', NULL, NULL, 1, 0, N'.00009.', NULL, 1, NULL, NULL, NULL, 42, 1, CAST(0x0000A30500317279 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A0161EF35 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, N'Về Chúng Tôi', N'Về Chúng Tôi', NULL, N'<p>Về Ch&uacute;ng T&ocirc;i</p>
', NULL, N'Ve-Chung-Toi', 1005, NULL, 1, 0, N'.00010.', NULL, 1, NULL, NULL, NULL, 44, 1, CAST(0x0000A30500318B1F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A01622094 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, N'Test 1.2.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-1-2-1', 1008, NULL, 1, 0, N'.00018.00019.00011.', 19, 1, NULL, NULL, NULL, 34, 1, CAST(0x0000A3050031ACF9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013CEF87 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (12, N'Test 2', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2', 1005, NULL, 1, 0, N'.00018.00019.00012.', 19, 1, NULL, NULL, NULL, 35, 1, CAST(0x0000A3050031CAB2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013C6725 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (13, N'Test 2.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2-1', 1005, NULL, 1, 0, N'.00018.00015.00013.', 15, 1, NULL, NULL, NULL, 2, 1, CAST(0x0000A3050031DCFB AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013D4425 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (14, N'Test 2.2', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2-2', 1005, NULL, 1, 0, N'.00018.00019.00012.00014.', 18, 1, NULL, NULL, NULL, 4, 1, CAST(0x0000A3050031EB8A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A315011980C2 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (15, N'Test 2.1.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2-1-1', 1005, NULL, 1, 0, N'.00018.00015.', 18, 1, NULL, NULL, NULL, 6, 1, CAST(0x0000A3050031FE8F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31400478D30 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (16, N'Test 2.1.2', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2-1-2', NULL, NULL, 1, 0, N'.00018.00015.00013.00016.', 13, 1, NULL, NULL, NULL, 7, 1, CAST(0x0000A3050032143D AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31301671C28 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (18, N'Test Another Page', NULL, NULL, N'<p>11</p>
', NULL, N'Test-Another-Page', 1005, NULL, 1, 0, N'.00018.', NULL, 1, NULL, NULL, NULL, 50, 1, CAST(0x0000A30500326629 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31B010F7B5E AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (19, N'Test 3', NULL, NULL, N'<p>1</p>
', NULL, N'Test-3', 1005, NULL, 1, 0, N'.00018.00019.', 18, 1, NULL, NULL, NULL, 7, 1, CAST(0x0000A3050032850E AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31301674E8D AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (20, N'Test 4', NULL, NULL, N'<p>1</p>
', NULL, N'Test-4', 1005, NULL, 1, 0, N'.00018.00020.', 19, 1, NULL, NULL, NULL, 11, 1, CAST(0x0000A30500329DC5 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3150110E57D AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [IsHomePage], [Hierarchy], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (21, N'Test 4.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-4-1', NULL, NULL, 1, 0, N'.00018.00019.00020.00021.', 20, 1, NULL, NULL, NULL, 5, 1, CAST(0x0000A3050032B250 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C01353F87 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[PageTags] ON 

GO
INSERT [dbo].[PageTags] ([Id], [PageId], [TagId]) VALUES (6, 5, 1)
GO
INSERT [dbo].[PageTags] ([Id], [PageId], [TagId]) VALUES (8, 5, 2)
GO
INSERT [dbo].[PageTags] ([Id], [PageId], [TagId]) VALUES (11, 12, 2)
GO
INSERT [dbo].[PageTags] ([Id], [PageId], [TagId]) VALUES (12, 12, 3)
GO
SET IDENTITY_INSERT [dbo].[PageTags] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

GO
INSERT [dbo].[News] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [IsHotNews], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Tiêu đề tin tức 5', N'test', N'<p>test 123</p>
', N'/Media/Folder2/baby_first.jpg', 2, 0, 0, 1, CAST(0x0000A2FF0160B0CC AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C016019D1 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[News] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [IsHotNews], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Tiêu đề tin tức 4', N'test 1123', N'<p>123</p>
', N'/Media/RotatingImages/hotelRoom3.jpg', 2, 1, 0, 1, CAST(0x0000A2FF01670233 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C01679D30 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[News] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [IsHotNews], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Tiêu đề tin tức 3', N'', N'', NULL, 1, 1, 0, 1, CAST(0x0000A2FF016709FB AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A016BFFE0 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[News] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [IsHotNews], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Tiêu đề tin tức 2', N'Pellentesque habitant morbi tristique senectus et netus et malesuada fames acasd asdasd turpis egestas asdsad asdasd Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas', N'<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames acasd asdasd turpis egestas asdsad asdasd Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas</p>
', N'/Media/News/news1.jpg', 1, 0, 0, 1, CAST(0x0000A3000006D10B AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C01641F5D AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[News] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [IsHotNews], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Tiêu đề tin tức 1', N'Pellentesque habitant morbi tristique senectus et netus et malesuada fames acasd asdasd turpis egestas asdsad asdasd Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas', N'<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames acasd asdasd turpis egestas asdsad asdasd Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas</p>
', N'/Media/News/news1.jpg', 1, 1, 0, 1, CAST(0x0000A3000007C16B AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A016C06F7 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[NewsCategories] ON 

GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'test', N'test 2
', NULL, N'.00001.', 1, 1, CAST(0x0000A2FB000E6077 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FB000E6972 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'test1', N'test1', 1, N'.00001.00002.', 2, 1, CAST(0x0000A2FF015ED868 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FF016A8449 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'test 3', N'1', NULL, N'.00003.', 1, 1, CAST(0x0000A2FF01685455 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FF0168546D AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'test 4', N'123', 2, N'.00001.00002.00004.', 123, 1, CAST(0x0000A2FF016860E7 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FF016A926B AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'test 44', N'123', NULL, N'.00005.', 5, 1, CAST(0x0000A30000027B3B AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A30000027B56 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[NewsCategories] ([Id], [Name], [Description], [ParentId], [Hierarchy], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'test 55', N'test', NULL, N'.00006.', 2, 1, CAST(0x0000A3000002894D AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A30000028961 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[NewsCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NewsNewsCategories] ON 

GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (66, 3, 1)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (67, 3, 3)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (68, 3, 2)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (72, 5, 1)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (73, 5, 3)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (74, 5, 2)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (77, 1, 3)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (78, 1, 4)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (79, 1, 6)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (83, 4, 3)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (84, 4, 2)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (85, 4, 4)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (86, 2, 2)
GO
INSERT [dbo].[NewsNewsCategories] ([Id], [NewsId], [NewsCategoryId]) VALUES (87, 2, 4)
GO
SET IDENTITY_INSERT [dbo].[NewsNewsCategories] OFF
GO
INSERT [dbo].[Languages] ([Id], [Name], [ShortName], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (N'en-US', N'English', N'America', 1, 1, CAST(0x0000A2F4010152B3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F40101A8B8 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Languages] ([Id], [Name], [ShortName], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (N'vi-VN', N'VietNam', N'VietNam', 2, 1, CAST(0x0000A2F4010152B3 AS DateTime), N'admninistrator', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SettingTypes] ON 

GO
INSERT [dbo].[SettingTypes] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'System', 1, 1, CAST(0x0000A2F3017F57F8 AS DateTime), N'administrator', CAST(0x0000A31A00E8222F AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SettingTypes] ([Id], [Name], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'BackEnd', 2, 1, CAST(0x0000A2FB0004B902 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A30F0165AA70 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[SettingTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'MaxSizeUploaded', N'10485760', 1, 2, 1, CAST(0x0000A2F4015A232D AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31700A7E058 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1002, N'PasswordSetting', N'{"PasswordMinLengthRequired":14,"PasswordMustHaveUpperAndLowerCaseLetters":true,"PasswordMustHaveDigit":false,"PasswordMustHaveSymbol":true}', 1, 0, 1, CAST(0x0000A2F7010C4BB2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A30B00E31459 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2002, N'CurlyBracket.MaxLoop', N'5', 1, 0, 1, CAST(0x0000A2FB0004C47F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2FD017A20CE AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2003, N'ImageUploadSetting', N'{"MinWidth":null,"MinHeight":null,"MaxWidth":null,"MaxHeight":null}', 1, 0, 1, CAST(0x0000A3030017E2AC AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A30E009FC99E AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2004, N'LogsPageSize', N'10', 2, 0, 1, CAST(0x0000A31700A57811 AS DateTime), N'system', CAST(0x0000A31700C87326 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2005, N'DefaultHistoryLength', N'5', 2, 0, 1, CAST(0x0000A318015EB51E AS DateTime), N'system', NULL, NULL)
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2006, N'DefaultHistoryStart', N'0', 2, 0, 1, CAST(0x0000A318015EB54E AS DateTime), N'system', NULL, NULL)
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2007, N'DefaultAddress', N'112 Phan Đình Phùng, Phường 6, TP. Đà Lạt', 2, 0, 1, CAST(0x0000A31900985070 AS DateTime), N'system', CAST(0x0000A319009BD955 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupPermissions] ON 

GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (2, 1, 2, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (3, 1, 3, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (4, 2, 1, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (5, 2, 2, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (6, 2, 3, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (7, 2, 4, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (8, 1, 4, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1002, 3, 1, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1003, 3, 2, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1004, 3, 3, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1005, 3, 4, 0)
GO
SET IDENTITY_INSERT [dbo].[GroupPermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[ClientMenus] ON 

GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Trang Chủ', 5, N'Home', NULL, N'.00001.', 1, NULL, NULL, 380, 1, CAST(0x0000A313013F42C8 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C0174475F AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Test', NULL, N'Test', NULL, N'.00002.', 1, CAST(0x0000A30000000000 AS DateTime), CAST(0x0000A31100000000 AS DateTime), 281, 1, CAST(0x0000A313014DB085 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3130164061A AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Dịch Vụ', 6, N'Dich-Vu', NULL, N'.00003.', 1, NULL, NULL, 390, 1, CAST(0x0000A31301650351 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A01617B91 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Tin Tức - Sự Kiện', 7, N'Tin-Tuc-Su-Kien', NULL, N'.00001.00004.', 1, NULL, NULL, 400, 1, CAST(0x0000A31301650C2F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C0186D1EA AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Test Another Page', 18, N'Test-Another-Page', NULL, N'.00005.', 1, NULL, NULL, 500, 1, CAST(0x0000A3130166C864 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31B010F7B80 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'Phòng Nghỉ', 8, N'Phong-Nghi', NULL, N'.00001.00006.', 1, NULL, NULL, 410, 1, CAST(0x0000A3130166DBD8 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A0161AF7A AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'Liên Hệ', 9, N'Lien-He', NULL, N'.00007.', 1, NULL, NULL, 420, 1, CAST(0x0000A3130166E671 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A0161F008 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'Về Chúng Tôi', 10, N'Ve-Chung-Toi', NULL, N'.00008.', 1, NULL, NULL, 440, 1, CAST(0x0000A3130166EA4D AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A01622144 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, N'Test 1.2.1', 11, N'Test-1-2-1', 16, N'.00005.00016.00009.', 1, NULL, NULL, 340, 1, CAST(0x0000A3130166F16A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013CEF99 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, N'Test 2', 12, N'Test-2', 16, N'.00005.00016.00010.', 1, NULL, NULL, 350, 1, CAST(0x0000A3130166F718 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013C6745 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, N'Test 2.1', 13, N'Test-2-1', 13, N'.00005.00013.00011.', 1, NULL, NULL, 20, 1, CAST(0x0000A3130166FFA8 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31C013D4438 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (13, N'Test 2.1.1', 15, N'Test-2-1-1', 5, N'.00005.00013.', 1, NULL, NULL, 60, 1, CAST(0x0000A313016713AF AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A315011980DC AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (14, N'Test 2.1.2', 16, N'Test-2-1-2', 11, N'.00005.00013.00011.00014.', 1, NULL, NULL, 60, 1, CAST(0x0000A31301671C63 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31400417924 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (16, N'Test 3', 19, N'Test-3', 5, N'.00005.00016.', 1, NULL, NULL, 70, 1, CAST(0x0000A31301674EC5 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A315011980EE AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[ClientMenus] ([Id], [Name], [PageId], [Url], [ParentId], [Hierarchy], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (17, N'Test 4', 20, N'Test-4', 16, N'.00005.00017.', 1, NULL, NULL, 110, 1, CAST(0x0000A313016755B4 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3150110E590 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[ClientMenus] OFF
GO
SET IDENTITY_INSERT [dbo].[PageLogs] ON 

GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, 6, N'Test curly bracket', N'123
1', N'123', N'<p>{Testimonial_5}</p>

<p>&nbsp;</p>
', N'<p>{Page}</p>
', N'Test-curly-bracket', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A316011F7FAD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, 6, N'Test curly bracket', N'Test caption', N'123', N'<p>{Testimonial_5}</p>

<p>Test</p>

<p>&nbsp;</p>
', N'<p>{Page}</p>
', N'Test-curly-bracket', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A316011F8E29 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, 6, N'Test curly bracket', N'Test caption', N'123', N'<p>{Testimonial_5}</p>

<p>Test</p>

<p>test</p>
', N'<p>{Page}</p>
', N'Test-curly-bracket', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
- Update field: Caption
', 0, 1, CAST(0x0000A316012054DF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, 6, N'Test curly bracket', N'Test caption', N'123', N'<p>{Testimonial_5}</p>

<p>Test</p>

<p>test test</p>
', N'<p>{Page}</p>
', N'Test-curly-bracket', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31700C7DAA5 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, 6, N'Test curly bracket', N'Test caption', N'123', N'<p>{Testimonial_5}</p>

<p>Test</p>

<p>test</p>
', N'<p>{Page}</p>
', N'Test-curly-bracket', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A3170116517C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, 5, N'Home', N'Home page test', NULL, N'<div id="bigCarousel" class="owl-carousel">
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="img/hotelRoom1.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="img/hotelRoom2.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="img/hotelRoom3.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="img/hotelRoom4.jpg" alt="">
            </a>
          </div>
        </div>
      
        <div class="divWrapperTwoHalf">
          <div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
            <div class="col-sm-12">
              <div class="headerType1HalfContent">HÂN HẠNH
                <div>KHÁCH HÀNG LÀ THƯỢNG ĐẾ SOME TEXT</div>
              </div>
            </div>
            <div class="clearfix"></div>
            <div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <a href="#">
                <img class="img-responsive img-thumbnail" src="img/hotelWelcome.jpg" />
              </a>
            </div>
            <div class="clearfix visible-xs visible-sm"></div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
              <div class="smallContentHeader">PHỤC VỤ TẬN TÌNH
                <div>Luôn đáp ứng mọi nhu cầu của khách hàng với thái độ tích cực, luôn luôn sẵn sàng, niềm nở, phục vụ 24/24. Với PXHotel, quý vị sẽ có những giây phút ấm áp bên người thân và gia đình.</div>
              </div>
              <button type="button" class="btn btn-primary">Đọc thêm</button>
            </div>
          </div>
          <div class="clearfix visible-xs"></div>
          <div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
            <div class="col-sm-12">
              <div class="headerType2HalfContent">PHÒNG NGHỈ
                <span>CAO CẤP</span>
              </div>
              <div>
                <img src="img/pattern1.gif" class="img-responsive center-block"></img>
              </div>
            </div>
            <div class="clearfix"></div>
            <div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <a href="#">
                <span class="squareOverlay"></span>
                <span class="squareMagnifier"></span>
                <img class="img-responsive img-thumbnail" src="img/squareImg1.jpg" />
                <div class="squareShortDetail">
                  <div class="squareRoomType">SUITE</div>
                  <div class="squareRoomPrice">1.800.000 VND</div>
                </div>
              </a>
            </div>
            <div class="clearfix visible-xs visible-sm"></div>
            <div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="img/squareImg2.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">STANDARD</div>
                    <div class="squareRoomPrice">500.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="img/squareImg3.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">SUPERIOR</div>
                    <div class="squareRoomPrice">700.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="clearfix"></div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="img/squareImg4.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">DELUXE</div>
                    <div class="squareRoomPrice">900.000 VND</div>
                  </div>
                  </a>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="img/squareImg5.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">SUITE</div>
                    <div class="squareRoomPrice">1.800.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="clearfix"></div>
              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <button type="button" class="btn btn-primary pull-right">Xem thêm</button>
              </div>
            </div>
          </div>
          <div class="clearfix visible-xs"></div>
        </div>

        <div class="clearfix"></div>

        <div class="adsOrPromo">
          <div class="adsTitle col-sm-8 col-sm-offset-1">ƯU ĐÃI ĐẶC BIỆT <span>CHO KHÁCH HÀNG VIP DIAMOND</span></div>
          <div class="clearfix"></div>
          <div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nhân dịp kỷ niệm 20 năm thành lập, trân trọng gửi đến quý khách hàng lâu năm thẻ VIP Diamond chương trình đặc biệt. Với mỗi hóa đơn từ 10.000.000 VNĐ, quý khách hàng sẽ được ưu đãi 50% giá trị. Dấu mốc đáng nhớ với PXHotel và quý vị! </div>
          <div class="clearfix"></div>
        </div>

        <div class="threePartsServices">
          <div class="col-sm-4 divNewsEvents">
            <img src="img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="img/border-top.png" class="img-responsive center-block">
            <div class="text-center">TIN TỨC - SỰ KIỆN</div>
            <img src="img/border-bot.png" class="img-responsive center-block">
            <ul class="listNews">
              <li class="listNewsItem">
                <div class="newsItemDate col-sm-3 col-xs-3">12
                  <div>Jan</div>
                </div>
                <div class="clearfix visible-xs"></div>
                <div class="newsItemContent col-sm-9">
                  <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
                </div>
              </li>
              <li class="listNewsItem">
                <div class="newsItemDate col-sm-3 col-xs-3">12
                  <div>Jan</div>
                </div>
                <div class="clearfix visible-xs"></div>
                <div class="newsItemContent col-sm-9">
                  <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
                </div>
              </li>
            </ul>
          </div>
          <div class="col-sm-4 divServices">
            <img src="img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="img/border-top.png" class="img-responsive center-block">
            <div class="text-center">DỊCH VỤ</div>
            <img src="img/border-bot.png" class="img-responsive center-block">
            <div class="carouselServiceList owl-carousel">
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="img/service1.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="img/service2.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="img/service3.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-sm-4 divBooking">
            <img src="img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="img/border-top.png" class="img-responsive center-block">
            <div class="text-center">ĐẶT PHÒNG TRỰC TUYẾN</div>
            <img src="img/border-bot.png" class="img-responsive center-block">
            <form class="form-horizontal onlineBookingForm" role="form">
              <div class="form-group">
                <label for="inputArrivalDate" class="col-sm-4 control-label">Ngày đến</label>
                <div class="col-sm-7">
                  <input type="date" class="form-control datepickerfield" id="inputArrivalDate" placeholder="">
                </div>
              </div>
              <div class="form-group">
                <label for="inputExitDate" class="col-sm-4 control-label">Ngày đi</label>
                <div class="col-sm-7">
                  <input type="date" class="form-control datepickerfield" id="inputExitDate" placeholder="">
                </div>
              </div>
              <div class="form-group">
                <div class="col-sm-offset-4 col-sm-7">
                  <button type="submit" class="btn btn-default">Kiểm tra</button>
                </div>
              </div>
            </form>
            <script type="text/javascript" src="http://www.skypeassets.com/i/scom/js/skype-uri.js"></script>
            <div id="SkypeButton_Call_iamnguyenhuykha_1" class="col-xs-6 col-sm-6 divSkypeOnline">
              <script type="text/javascript">
                Skype.ui({
                  "name": "chat",
                  "element": "SkypeButton_Call_iamnguyenhuykha_1",
                  "participants": ["iamnguyenhuykha"],
                  "imageSize": 32
                });
              </script>
            </div>
            <!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->
            <script type="text/javascript" src="http://www.skypeassets.com/i/scom/js/skype-uri.js"></script>
            <div id="SkypeButton_Call_liemdl_1" class="col-xs-6 col-sm-6 divSkypeOnline">
              <script type="text/javascript">
                Skype.ui({
                  "name": "call",
                  "element": "SkypeButton_Call_liemdl_1",
                  "participants": ["liemdl"],
                  "imageSize": 32
                });
              </script>
            </div>
            <div class="clearfix"></div>
          </div>
          <div class="clearfix"></div>
        </div>', NULL, N'Home', 1005, NULL, 1, NULL, 1, CAST(0x0000A30800000000 AS DateTime), CAST(0x0000A31800000000 AS DateTime), NULL, N'** Create Page **', 0, 1, CAST(0x0000A31900B9136E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, 5, N'Home', N'Home page test', NULL, N'<div class="owl-carousel" id="bigCarousel">
<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>
</div>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<div class="carouselServiceList owl-carousel">
<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service1.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service2.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service3.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>
</div>
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, CAST(0x0000A30800000000 AS DateTime), CAST(0x0000A31800000000 AS DateTime), NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31900C2BFBD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (12, 5, N'Home', N'Home page test', NULL, N'
        <div id="bigCarousel" class="owl-carousel">
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom1.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom2.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom3.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom4.jpg" alt="">
            </a>
          </div>
        </div>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<div class="carouselServiceList owl-carousel">
<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service1.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service2.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service3.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>
</div>
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, CAST(0x0000A30800000000 AS DateTime), CAST(0x0000A31800000000 AS DateTime), NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31900C33C9F AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (13, 5, N'Home', N'Home page test', NULL, N'<div id="bigCarousel" class="owl-carousel">
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom1.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom2.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom3.jpg" alt="">
            </a>
          </div>
          <div class="item">
            <a href="#">
              <img class="lazyOwl" data-src="/Content/FrontEnd/img/hotelRoom4.jpg" alt="">
            </a>
          </div>
        </div>
      
        <div class="divWrapperTwoHalf">
          <div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
            <div class="col-sm-12">
              <div class="headerType1HalfContent">HÂN HẠNH
                <div>KHÁCH HÀNG LÀ THƯỢNG ĐẾ SOME TEXT</div>
              </div>
            </div>
            <div class="clearfix"></div>
            <div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <a href="#">
                <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" />
              </a>
            </div>
            <div class="clearfix visible-xs visible-sm"></div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
              <div class="smallContentHeader">PHỤC VỤ TẬN TÌNH
                <div>Luôn đáp ứng mọi nhu cầu của khách hàng với thái độ tích cực, luôn luôn sẵn sàng, niềm nở, phục vụ 24/24. Với PXHotel, quý vị sẽ có những giây phút ấm áp bên người thân và gia đình.</div>
              </div>
              <button type="button" class="btn btn-primary">Đọc thêm</button>
            </div>
          </div>
          <div class="clearfix visible-xs"></div>
          <div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
            <div class="col-sm-12">
              <div class="headerType2HalfContent">PHÒNG NGHỈ
                <span>CAO CẤP</span>
              </div>
              <div>
                <img src="/Content/FrontEnd/img/pattern1.gif" class="img-responsive center-block"></img>
              </div>
            </div>
            <div class="clearfix"></div>
            <div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <a href="#">
                <span class="squareOverlay"></span>
                <span class="squareMagnifier"></span>
                <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" />
                <div class="squareShortDetail">
                  <div class="squareRoomType">SUITE</div>
                  <div class="squareRoomPrice">1.800.000 VND</div>
                </div>
              </a>
            </div>
            <div class="clearfix visible-xs visible-sm"></div>
            <div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">STANDARD</div>
                    <div class="squareRoomPrice">500.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">SUPERIOR</div>
                    <div class="squareRoomPrice">700.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="clearfix"></div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">DELUXE</div>
                    <div class="squareRoomPrice">900.000 VND</div>
                  </div>
                  </a>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <a href="#">
                  <span class="squareOverlay"></span>
                  <span class="squareMagnifier"></span>
                  <img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" />
                  <div class="squareShortDetail">
                    <div class="squareRoomType">SUITE</div>
                    <div class="squareRoomPrice">1.800.000 VND</div>
                  </div>
                </a>
              </div>
              <div class="clearfix"></div>
              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <button type="button" class="btn btn-primary pull-right">Xem thêm</button>
              </div>
            </div>
          </div>
          <div class="clearfix visible-xs"></div>
        </div>

        <div class="clearfix"></div>

        <div class="adsOrPromo">
          <div class="adsTitle col-sm-8 col-sm-offset-1">ƯU ĐÃI ĐẶC BIỆT <span>CHO KHÁCH HÀNG VIP DIAMOND</span></div>
          <div class="clearfix"></div>
          <div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nhân dịp kỷ niệm 20 năm thành lập, trân trọng gửi đến quý khách hàng lâu năm thẻ VIP Diamond chương trình đặc biệt. Với mỗi hóa đơn từ 10.000.000 VNĐ, quý khách hàng sẽ được ưu đãi 50% giá trị. Dấu mốc đáng nhớ với PXHotel và quý vị! </div>
          <div class="clearfix"></div>
        </div>

        <div class="threePartsServices">
          <div class="col-sm-4 divNewsEvents">
            <img src="/Content/FrontEnd/img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="/Content/FrontEnd/img/border-top.png" class="img-responsive center-block">
            <div class="text-center">TIN TỨC - SỰ KIỆN</div>
            <img src="/Content/FrontEnd/img/border-bot.png" class="img-responsive center-block">
            <ul class="listNews">
              <li class="listNewsItem">
                <div class="newsItemDate col-sm-3 col-xs-3">12
                  <div>Jan</div>
                </div>
                <div class="clearfix visible-xs"></div>
                <div class="newsItemContent col-sm-9">
                  <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
                </div>
              </li>
              <li class="listNewsItem">
                <div class="newsItemDate col-sm-3 col-xs-3">12
                  <div>Jan</div>
                </div>
                <div class="clearfix visible-xs"></div>
                <div class="newsItemContent col-sm-9">
                  <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem thêm</a>
                </div>
              </li>
            </ul>
          </div>
          <div class="col-sm-4 divServices">
            <img src="/Content/FrontEnd/img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="/Content/FrontEnd/img/border-top.png" class="img-responsive center-block">
            <div class="text-center">DỊCH VỤ</div>
            <img src="/Content/FrontEnd/img/border-bot.png" class="img-responsive center-block">
            <div class="carouselServiceList owl-carousel">
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="/Content/FrontEnd/img/service1.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="/Content/FrontEnd/img/service2.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
              <div class="item">
                <div class="serviceList">
                  <div class="serviceImg col-sm-6">
                    <img src="/Content/FrontEnd/img/service3.jpg" class="img-thumbnail img-responsive"/>
                  </div>
                  <div class="serviceDescription col-sm-6">
                    <div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
                    <button type="button" class="btn btn-default">Đọc thêm</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-sm-4 divBooking">
            <img src="/Content/FrontEnd/img/pattern1.png" class="imgPattern img-responsive center-block">
            <img src="/Content/FrontEnd/img/border-top.png" class="img-responsive center-block">
            <div class="text-center">ĐẶT PHÒNG TRỰC TUYẾN</div>
            <img src="/Content/FrontEnd/img/border-bot.png" class="img-responsive center-block">
            <form class="form-horizontal onlineBookingForm" role="form">
              <div class="form-group">
                <label for="inputArrivalDate" class="col-sm-4 control-label">Ngày đến</label>
                <div class="col-sm-7">
                  <input type="date" class="form-control datepickerfield" id="inputArrivalDate" placeholder="">
                </div>
              </div>
              <div class="form-group">
                <label for="inputExitDate" class="col-sm-4 control-label">Ngày đi</label>
                <div class="col-sm-7">
                  <input type="date" class="form-control datepickerfield" id="inputExitDate" placeholder="">
                </div>
              </div>
              <div class="form-group">
                <div class="col-sm-offset-4 col-sm-7">
                  <button type="submit" class="btn btn-default">Kiểm tra</button>
                </div>
              </div>
            </form>
            <script type="text/javascript" src="http://www.skypeassets.com/i/scom/js/skype-uri.js"></script>
            <div id="SkypeButton_Call_iamnguyenhuykha_1" class="col-xs-6 col-sm-6 divSkypeOnline">
              <script type="text/javascript">
                Skype.ui({
                  "name": "chat",
                  "element": "SkypeButton_Call_iamnguyenhuykha_1",
                  "participants": ["iamnguyenhuykha"],
                  "imageSize": 32
                });
              </script>
            </div>
            <!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->
            <script type="text/javascript" src="http://www.skypeassets.com/i/scom/js/skype-uri.js"></script>
            <div id="SkypeButton_Call_liemdl_1" class="col-xs-6 col-sm-6 divSkypeOnline">
              <script type="text/javascript">
                Skype.ui({
                  "name": "call",
                  "element": "SkypeButton_Call_liemdl_1",
                  "participants": ["liemdl"],
                  "imageSize": 32
                });
              </script>
            </div>
            <div class="clearfix"></div>
          </div>
          <div class="clearfix"></div>
        </div>', NULL, N'Home', 1005, NULL, 1, NULL, 1, CAST(0x0000A30800000000 AS DateTime), CAST(0x0000A31800000000 AS DateTime), NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31900C4010E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (14, 5, N'Home', N'Home page test', NULL, N'<div class="owl-carousel" id="bigCarousel">
<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>
</div>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<div class="carouselServiceList owl-carousel">
<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service1.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service2.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service3.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>
</div>
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31900CDD88E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (15, 5, N'Home2', N'Home page test', NULL, N'<div class="owl-carousel" id="bigCarousel">
<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>

<div class="item">&nbsp;</div>
</div>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<div class="carouselServiceList owl-carousel">
<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service1.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service2.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service3.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>
</div>
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
- Update field: StartPublishingDate
- Update field: EndPublishingDate
', 0, 1, CAST(0x0000A319010B058D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (16, 5, N'Home', N'Home page test', NULL, N'{RotatingImages_2}

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<div class="carouselServiceList owl-carousel">
<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service1.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service2.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>

<div class="item">
<div class="serviceList">
<div class="serviceImg col-sm-6"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/service3.jpg" /></div>

<div class="serviceDescription col-sm-6">
<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</div>
Đọc th&ecirc;m</div>
</div>
</div>
</div>
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Title
', 0, 1, CAST(0x0000A3190127BD11 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (17, 5, N'Home', N'Home page test', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
<a href="#"> </a></div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<ul>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
	<li>
	<div class="newsItemDate col-sm-3 col-xs-3">12
	<div>Jan</div>
	</div>

	<div class="clearfix visible-xs">&nbsp;</div>

	<div class="newsItemContent col-sm-9">
	<div>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas...<a href="#">Xem th&ecirc;m</a></div>
	</div>
	</li>
</ul>
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
{Services_3}
</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Title
- Update field: Content
', 0, 1, CAST(0x0000A31A00FAD565 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (18, 5, N'Home', N'Home page test', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="adsOrPromo">
<div class="adsTitle col-sm-8 col-sm-offset-1">ƯU Đ&Atilde;I ĐẶC BIỆT CHO KH&Aacute;CH H&Agrave;NG VIP DIAMOND</div>

<div class="clearfix">&nbsp;</div>

<div class="adsDescription col-sm-8 col-sm-offset-1">PXHotel nh&acirc;n dịp kỷ niệm 20 năm th&agrave;nh lập, tr&acirc;n trọng gửi đến qu&yacute; kh&aacute;ch h&agrave;ng l&acirc;u năm thẻ VIP Diamond chương tr&igrave;nh đặc biệt. Với mỗi h&oacute;a đơn từ 10.000.000 VNĐ, qu&yacute; kh&aacute;ch h&agrave;ng sẽ được ưu đ&atilde;i 50% gi&aacute; trị. Dấu mốc đ&aacute;ng nhớ với PXHotel v&agrave; qu&yacute; vị!</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
{News_2}
</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A01000623 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (19, 8, N'Test 1.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-123', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31A01062991 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (20, 8, N'Test 1.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-123', 1005, NULL, 1, 5, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: ParentId
', 0, 1, CAST(0x0000A31A0106346D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (21, 5, N'Home', N'Home page test', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

{Banner_1}
<div class="clearfix">&nbsp;</div>
</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {News_2}</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A0151AFF5 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (22, 5, N'Home', N'Home page test', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<p>{Banner_1}</p>

<div class="clearfix">&nbsp;</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {News_2}</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
<div class="divFeedbackAndGallery">
          <div class="col-lg-6">
            <div class="col-lg-7 divWrapperTestimonial">
              {Testimonials}
            </div>
            <div class="col-lg-5 divWrapperFeedback">
              <div class="textHeadingDivFeedback col-lg-12">
                Ý KIẾN
              </div>
              <form role="form">
                <div class="form-group">
                  <input type="text" class="form-control" id="inputCustomerNameFeedback" placeholder="Tên Quý Khách">
                </div>
                <div class="form-group">
                  <textarea class="form-control" rows="3" placeholder="Nội dung"></textarea>
                </div>
                <button type="submit" class="btn btn-default">Gửi phản hồi</button>
              </form>
            </div>
          </div>
          <div class="col-lg-3 divWrapperContact">
            <div class="textHeadingDivFeedback col-lg-12">
              LIÊN HỆ
            </div>
            <ul class="divContactNearFooter">
              <li>
                <i class="fa fa-map-marker"></i>
                <p> 
                   <strong>Địa chỉ:</strong> 112 Phan Đình Phùng, Phường 6, TP. Đà Lạt
                </p>
              </li>
              <li>
                <i class="fa fa-phone"></i>
                <p><strong>Phone:</strong> +84 63 00000</p>
              </li>
              <li>
                <i class="fa fa-envelope"></i>
                <p><strong>Email:</strong><a href="mailto:pxmail@yahoo.com">pxmail@yahoo.com</a></p>
              </li>
            </ul>
          </div>
          <div class="col-lg-3 divWrapperImageGallery">
            <div class="textHeadingDivFeedback col-lg-12">
              THƯ VIỆN ẢNH
            </div>
            <div id=''gallery''>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg1.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg1.jpg" />
                </a>
              </div>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg2.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg2.jpg" />
                </a>
              </div>
              <div class="clearfix visible-xs"></div>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg3.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg3.jpg" />
                </a>
              </div>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg4.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg4.jpg" />
                </a>
              </div>
              <div class="clearfix visible-xs"></div>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg5.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg5.jpg" />
                </a>
              </div>
              <div class="imageInGallery col-sm-4 col-xs-6">
                <a href="/Content/FrontEnd/img/galleryImg6.jpg">
                  <img class="img-responsive" src="/Content/FrontEnd/img/galleryImg6.jpg" />
                </a>
              </div>
              <div class="clearfix visible-xs"></div>
            </div>
          </div>
          <div class="clearfix"></div>
        </div>', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A015B9A7B AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (23, 5, N'Trang Chủ', N'Home page', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<p>{Banner_1}</p>

<div class="clearfix">&nbsp;</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {News_2}</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="divFeedbackAndGallery">
<div class="col-lg-6">
<div class="col-lg-7 divWrapperTestimonial">{Testimonials}</div>

<div class="col-lg-5 divWrapperFeedback">
<div class="textHeadingDivFeedback col-lg-12">&Yacute; KIẾN</div>

<form>
<div class="form-group"><input type="text" /></div>

<div class="form-group"><textarea rows="3"></textarea></div>
Gửi phản hồi</form>
</div>
</div>

<div class="col-lg-3 divWrapperContact">
<div class="textHeadingDivFeedback col-lg-12">LI&Ecirc;N HỆ</div>

<ul>
	<li>
	<p><strong>Địa chỉ:</strong> 112 Phan Đ&igrave;nh Ph&ugrave;ng, Phường 6, TP. Đ&agrave; Lạt</p>
	</li>
	<li>
	<p><strong>Phone:</strong> +84 63 00000</p>
	</li>
	<li>
	<p><strong>Email:</strong><a href="mailto:pxmail@yahoo.com">pxmail@yahoo.com</a></p>
	</li>
</ul>
</div>

<div class="col-lg-3 divWrapperImageGallery">
<div class="textHeadingDivFeedback col-lg-12">THƯ VIỆN ẢNH</div>

<div id="gallery">
<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg1.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg1.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg2.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg2.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg3.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg3.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg4.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg4.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg5.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg5.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg6.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg6.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A015FE307 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (24, 6, N'Dịch Vụ', N'Dịch vụ', N'123', N'<p>{Testimonials_5}</p>
', N'<p>{Page}</p>
', N'Dich-Vu', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A0161009E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (25, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<p>Tin tức</p>
', NULL, N'Tin-Tuc-Su-Kien', NULL, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31A0161599F AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (26, 6, N'Dịch Vụ', N'Dịch vụ', N'123', N'<p>{Testimonials_5}</p>
', N'<p>{Page}</p>
', N'Dich-Vu', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Title
- Update field: FriendlyUrl
- Update field: Content
- Update field: Caption
', 0, 1, CAST(0x0000A31A01617BCE AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (27, 8, N'Test 1.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-123', 1005, NULL, 1, 5, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: ParentId
', 0, 1, CAST(0x0000A31A0161AFB7 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (28, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<p>Tin tức</p>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Title
- Update field: FriendlyUrl
- Update field: Content
- Update field: Caption
- Update field: ParentId
', 0, 1, CAST(0x0000A31A0161C13B AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (29, 9, N'Test 1.1.1', NULL, NULL, N'<p>11</p>
', NULL, N'Test-1-1-1', NULL, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31A0161F045 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (30, 10, N'Test 1.2', NULL, NULL, N'<p>11</p>
', NULL, N'Test-1-2', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31A01622183 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (31, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
          <div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
            <h1>Tin tức</h1>
            <span>
              News
            </span>
          </div>
        </div>

        <div class="divWrapperRoomBookingOnlineAndContact">
          <div class="col-sm-8">
            <ul class="blog-list">
              <li>
                <div class="info">
                  <div class="date">
                    <h4>22</h4>
                    <span>SEP</span>
                  </div>
                </div>
                <div class="preview">
                  <img class="img-responsive" src="img/news1.jpg" />
                  <a href="#">
                    <h3 class="blog-title">Tiêu đề tin tức 1</h3>
                  </a>
                  <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">Admin</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    <a href="#">Sự kiện</a>
                    ,
                    <a href="#">Khuyến mãi</a>
                    <span>|</span>
                  </div>
                  <div class="short-description">Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi. Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi.</div>
                  <a href="#">
                    <button class="blog-readmore-button">
                      Đọc thêm <i class="fa fa-eye"></i>
                    </button>
                  </a>
                  <div class="blog-comments">
                    <i class="fa fa-comments"></i> Bình luận: 16
                  </div>
                </div>
              </li>
              <li>
                <div class="info">
                  <div class="date">
                    <h4>22</h4>
                    <span>SEP</span>
                  </div>
                </div>
                <div class="preview">
                  <img class="img-responsive" src="img/news1.jpg" />
                  <a href="#">
                    <h3 class="blog-title">Tiêu đề tin tức 1</h3>
                  </a>
                  <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">Admin</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    <a href="#">Sự kiện</a>
                    ,
                    <a href="#">Khuyến mãi</a>
                    <span>|</span>
                  </div>
                  <div class="short-description">Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi. Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi.</div>
                  <a href="#">
                    <button class="blog-readmore-button">
                      Đọc thêm <i class="fa fa-eye"></i>
                    </button>
                  </a>
                  <div class="blog-comments">
                    <i class="fa fa-comments"></i> Bình luận: 16
                  </div>
                </div>
              </li>
              <li>
                <div class="info">
                  <div class="date">
                    <h4>22</h4>
                    <span>SEP</span>
                  </div>
                </div>
                <div class="preview">
                  <img class="img-responsive" src="img/news1.jpg" />
                  <a href="#">
                    <h3 class="blog-title">Tiêu đề tin tức 1</h3>
                  </a>
                  <div class="meta-info">
                    <i class="fa fa-user"></i>
                    <a href="#">Admin</a>
                    <span>|</span>
                    <i class="fa fa-tag"></i>
                    <a href="#">Sự kiện</a>
                    ,
                    <a href="#">Khuyến mãi</a>
                    <span>|</span>
                  </div>
                  <div class="short-description">Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi. Đây là đoạn mô tả ngắn gọn, hay còn gọi là short description, của một tin tức được đăng lên. Nó thể hiện đại ý của bài viết, tóm tắt thông tin tổng quan để dễ theo dõi.</div>
                  <a href="#">
                    <button class="blog-readmore-button">
                      Đọc thêm <i class="fa fa-eye"></i>
                    </button>
                  </a>
                  <div class="blog-comments">
                    <i class="fa fa-comments"></i> Bình luận: 16
                  </div>
                </div>
              </li>
            </ul>
            <div class="blog-page-navigation-wrap">
              <ul>
                <li class="prev">
                  <a href="#"><span class="fa fa-angle-left"></span> Prev                        
                  </a>
                </li>
                <li class="current">
                  <a href="#">1</a>
                </li>
                <li>
                  <a href="#">2</a>
                </li>
                <li>
                  <a href="#">3</a>
                </li>
                <li class="next">
                  <a href="#">Next  
                    <span class="fa fa-angle-right"></span>
                  </a>
                </li>
              </ul>
            </div>
            <div class="clearfix"></div>
          </div>
          <div class="col-sm-4">
            <div class="widget latest_news">
              <h4 class="title">Tin mới nhất</h4>
              <ul class="list-news">
                <li>
                  <img class="img-responsive img-thumbnail" alt="" src="img/thumb1.jpg">
                  <div class="text">
                    <h5>
                      <a href="img/#">Khuyến mãi dịp 30/4</a>
                    </h5>
                    Hãy đến với PX Hotel dịp lễ 30/4, Quý khách sẽ được hưởng ưu đãi lên đến 50%. Cơ hội không thể tuyệt vời hơn.
                  </div>
                </li>
                <li>
                  <img class="img-responsive img-thumbnail" alt="" src="img/thumb1.jpg">
                  <div class="text">
                    <h5>
                      <a href="img/#">Nghỉ dưỡng tại PX Hotel</a>
                    </h5>
                    Quý khách sẽ có khoảng thời gian nghỉ dưỡng tuyệt vời tại Đà Lạt khi ở tại PX Hotel
                  </div>
                </li>
              </ul>
            </div>
            <div class="widget widget_category">
              <h4 class="title">Phân loại</h4>
              <div class="table-responsive">
                <table class="table table-hovered">
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Sự kiện</a><span class="cateCount">(5)</span></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tin tức</a><span class="cateCount">(3)</span></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Nghĩ Lễ</a><span class="cateCount">(7)</span></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Khuyến mãi</a><span class="cateCount">(4)</span></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Video</a><span class="cateCount">(2)</span></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tổng hợp</a><span class="cateCount">(1)</span></td>
                  </tr>
                </table>
              </div>
            </div>
            <div class="widget widget_tags">
              <h4 class="title">Tags</h4>
              <span>
                <a href="#">khách sạn</a>
              </span>
              <span>
                <a href="#">khuyến mãi</a>
              </span>
              <span>
                <a href="#">sự kiện</a>
              </span>
              <span>
                <a href="#">ăn uống</a>
              </span>
              <span>
                <a href="#">tiện ích</a>
              </span>
            </div>
            <div class="widget widget_category">
              <h4 class="title">Thời gian</h4>
              <div class="table-responsive">
                <table class="table table-hovered">
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tháng Chín 2013</a></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tháng Mười Một 2013</a></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tháng Hai 2014</a></td>
                  </tr>
                  <tr>
                    <td><i class="fa fa-angle-right"></i><a href="#">Tháng Ba 2014</a></td>
                  </tr>
                </table>
              </div>
            </div>
          </div>
          <div class="clearfix"></div>
        </div>', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: PageTemplateId
', 0, 1, CAST(0x0000A31A0162FFA0 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (32, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
  <div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
	<h1>Tin tức</h1>
	<span>
	  News
	</span>
  </div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
  <div class="col-sm-8">
	{NewsListing}
  </div>
  <div class="col-sm-4">
	<div class="widget latest_news">
	  <h4 class="title">Tin mới nhất</h4>
	  <ul class="list-news">
		<li>
		  <img class="img-responsive img-thumbnail" alt="" src="img/thumb1.jpg">
		  <div class="text">
			<h5>
			  <a href="img/#">Khuyến mãi dịp 30/4</a>
			</h5>
			Hãy đến với PX Hotel dịp lễ 30/4, Quý khách sẽ được hưởng ưu đãi lên đến 50%. Cơ hội không thể tuyệt vời hơn.
		  </div>
		</li>
		<li>
		  <img class="img-responsive img-thumbnail" alt="" src="img/thumb1.jpg">
		  <div class="text">
			<h5>
			  <a href="img/#">Nghỉ dưỡng tại PX Hotel</a>
			</h5>
			Quý khách sẽ có khoảng thời gian nghỉ dưỡng tuyệt vời tại Đà Lạt khi ở tại PX Hotel
		  </div>
		</li>
	  </ul>
	</div>
	<div class="widget widget_category">
	  <h4 class="title">Phân loại</h4>
	  <div class="table-responsive">
		<table class="table table-hovered">
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Sự kiện</a><span class="cateCount">(5)</span></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tin tức</a><span class="cateCount">(3)</span></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Nghĩ Lễ</a><span class="cateCount">(7)</span></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Khuyến mãi</a><span class="cateCount">(4)</span></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Video</a><span class="cateCount">(2)</span></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tổng hợp</a><span class="cateCount">(1)</span></td>
		  </tr>
		</table>
	  </div>
	</div>
	<div class="widget widget_tags">
	  <h4 class="title">Tags</h4>
	  <span>
		<a href="#">khách sạn</a>
	  </span>
	  <span>
		<a href="#">khuyến mãi</a>
	  </span>
	  <span>
		<a href="#">sự kiện</a>
	  </span>
	  <span>
		<a href="#">ăn uống</a>
	  </span>
	  <span>
		<a href="#">tiện ích</a>
	  </span>
	</div>
	<div class="widget widget_category">
	  <h4 class="title">Thời gian</h4>
	  <div class="table-responsive">
		<table class="table table-hovered">
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tháng Chín 2013</a></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tháng Mười Một 2013</a></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tháng Hai 2014</a></td>
		  </tr>
		  <tr>
			<td><i class="fa fa-angle-right"></i><a href="#">Tháng Ba 2014</a></td>
		  </tr>
		</table>
	  </div>
	</div>
  </div>
  <div class="clearfix"></div>
</div>', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31A01692F81 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (33, 11, N'Test 1.2.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-1-2-1', 1008, NULL, 1, 10, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31B00D75637 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (34, 18, N'Test Another Page', NULL, NULL, N'<p>11</p>
', NULL, N'Test-Another-Page', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31B010F7BC0 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (35, 21, N'Test 4.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-4-1', NULL, NULL, 1, 20, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31C01353FED AS DateTime), N'iamnguyenhuykha@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (36, 12, N'Test 2', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2', 1005, NULL, 1, 6, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31C013C6774 AS DateTime), N'iamnguyenhuykha@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (37, 11, N'Test 1.2.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-1-2-1', 1008, NULL, 1, 10, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: PageTemplateId
', 0, 1, CAST(0x0000A31C013CEFA4 AS DateTime), N'iamnguyenhuykha@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (38, 13, N'Test 2.1', NULL, NULL, N'<p>1</p>
', NULL, N'Test-2-1', 1005, NULL, 1, 12, 1, NULL, NULL, NULL, N'** Create Page **', 0, 1, CAST(0x0000A31C013D4446 AS DateTime), N'iamnguyenhuykha@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (39, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
News</div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>

<div class="col-sm-4">
{HotNews_2}

<div class="widget widget_category">
<h4>Ph&acirc;n loại</h4>

<div class="table-responsive">
<table class="table table-hovered">
	<tbody>
		<tr>
			<td><a href="#">Sự kiện</a>(5)</td>
		</tr>
		<tr>
			<td><a href="#">Tin tức</a>(3)</td>
		</tr>
		<tr>
			<td><a href="#">Nghĩ Lễ</a>(7)</td>
		</tr>
		<tr>
			<td><a href="#">Khuyến m&atilde;i</a>(4)</td>
		</tr>
		<tr>
			<td><a href="#">Video</a>(2)</td>
		</tr>
		<tr>
			<td><a href="#">Tổng hợp</a>(1)</td>
		</tr>
	</tbody>
</table>
</div>
</div>

<div class="widget widget_tags">
<h4>Tags</h4>
<a href="#">kh&aacute;ch sạn</a> <a href="#">khuyến m&atilde;i</a> <a href="#">sự kiện</a> <a href="#">ăn uống</a> <a href="#">tiện &iacute;ch</a></div>

<div class="widget widget_category">
<h4>Thời gian</h4>

<div class="table-responsive">
<table class="table table-hovered">
	<tbody>
		<tr>
			<td><a href="#">Th&aacute;ng Ch&iacute;n 2013</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Mười Một 2013</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Hai 2014</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Ba 2014</a></td>
		</tr>
	</tbody>
</table>
</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31C016E1FE0 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (40, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
News</div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>

<div class="col-sm-4">{HotNews_2}
{CategoryListing}

<div class="widget widget_tags">
<h4>Tags</h4>
<a href="#">kh&aacute;ch sạn</a> <a href="#">khuyến m&atilde;i</a> <a href="#">sự kiện</a> <a href="#">ăn uống</a> <a href="#">tiện &iacute;ch</a></div>

<div class="widget widget_category">
<h4>Thời gian</h4>

<div class="table-responsive">
<table class="table table-hovered">
	<tbody>
		<tr>
			<td><a href="#">Th&aacute;ng Ch&iacute;n 2013</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Mười Một 2013</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Hai 2014</a></td>
		</tr>
		<tr>
			<td><a href="#">Th&aacute;ng Ba 2014</a></td>
		</tr>
	</tbody>
</table>
</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31C016FB374 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (41, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
News</div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>

<div class="col-sm-4">{HotNews_2} {CategoryListing}
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31C0171FEEF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (42, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
<span>News</span></div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>

<div class="col-sm-4">{HotNews_2} {CategoryListing}</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Tin-Tuc-Su-Kien', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31C017388A7 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (43, 5, N'Trang Chủ', N'Home page', NULL, N'<p>{RotatingImages_2}</p>

<div class="divWrapperTwoHalf">
<div class="divHalfContentWrapper halfOnLeft col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType1HalfContent">H&Acirc;N HẠNH
<div>KH&Aacute;CH H&Agrave;NG L&Agrave; THƯỢNG ĐẾ SOME TEXT</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="thumbnailWithShadowWrapper col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/hotelWelcome.jpg" /> </a></div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 rightSmallContent">
<div class="smallContentHeader">PHỤC VỤ TẬN T&Igrave;NH
<div>Lu&ocirc;n đ&aacute;p ứng mọi nhu cầu của kh&aacute;ch h&agrave;ng với th&aacute;i độ t&iacute;ch cực, lu&ocirc;n lu&ocirc;n sẵn s&agrave;ng, niềm nở, phục vụ 24/24. Với PXHotel, qu&yacute; vị sẽ c&oacute; những gi&acirc;y ph&uacute;t ấm &aacute;p b&ecirc;n người th&acirc;n v&agrave; gia đ&igrave;nh.</div>
</div>
Đọc th&ecirc;m</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="divHalfContentWrapper halfOnRight col-xs-12 col-sm-6">
<div class="col-sm-12">
<div class="headerType2HalfContent">PH&Ograve;NG NGHỈ CAO CẤP</div>

<div><img class="center-block img-responsive" src="/Content/FrontEnd/img/pattern1.gif" /></div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="divBigSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg1.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix visible-xs visible-sm">&nbsp;</div>

<div class="divSmallSquareImg col-xs-12 col-sm-12 col-md-6 col-lg-6">
<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg2.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">STANDARD</a></div>

<div class="squareRoomPrice"><a href="#">500.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg3.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUPERIOR</a></div>

<div class="squareRoomPrice"><a href="#">700.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg4.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">DELUXE</a></div>

<div class="squareRoomPrice"><a href="#">900.000 VND</a></div>
</div>
</div>

<div class="col-xs-6 col-sm-6 col-md-6 col-lg-6"><a href="#"><img class="img-responsive img-thumbnail" src="/Content/FrontEnd/img/squareImg5.jpg" /> </a>

<div class="squareShortDetail">
<div class="squareRoomType"><a href="#">SUITE</a></div>

<div class="squareRoomPrice"><a href="#">1.800.000 VND</a></div>
</div>
</div>

<div class="clearfix">&nbsp;</div>

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Xem th&ecirc;m</div>
</div>
</div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>

<p>{Banner_1}</p>

<div class="clearfix">&nbsp;</div>

<div class="threePartsServices">
<div class="col-sm-4 divNewsEvents"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">TIN TỨC - SỰ KIỆN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {News_2}</div>

<div class="col-sm-4 divServices"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">DỊCH VỤ</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" /> {Services_3}</div>

<div class="col-sm-4 divBooking"><img class="center-block img-responsive imgPattern" src="/Content/FrontEnd/img/pattern1.png" /> <img class="center-block img-responsive" src="/Content/FrontEnd/img/border-top.png" />
<div class="text-center">ĐẶT PH&Ograve;NG TRỰC TUYẾN</div>
<img class="center-block img-responsive" src="/Content/FrontEnd/img/border-bot.png" />
<form>
<div class="form-group">Ng&agrave;y đến
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">Ng&agrave;y đi
<div class="col-sm-7"><input type="date" /></div>
</div>

<div class="form-group">
<div class="col-sm-offset-4 col-sm-7">Kiểm tra</div>
</div>
</form>

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_iamnguyenhuykha_1">&nbsp;</div>
<!-- <a href="ymsgr:sendIM?iamnguyenhuykha"><img border="0" src="http://opi.yahoo.com/online?u=iamnguyenhuykha&m=g&t=9&l=us"></a> -->

<div class="col-xs-6 col-sm-6 divSkypeOnline" id="SkypeButton_Call_liemdl_1">&nbsp;</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>

<div class="divFeedbackAndGallery">
<div class="col-lg-6">
<div class="col-lg-7 divWrapperTestimonial">{Testimonials}</div>

<div class="col-lg-5 divWrapperFeedback">
<div class="textHeadingDivFeedback col-lg-12">&Yacute; KIẾN</div>

<form>
<div class="form-group"><input type="text" /></div>

<div class="form-group"><textarea rows="3"></textarea></div>
Gửi phản hồi</form>
</div>
</div>

<div class="col-lg-3 divWrapperContact">
<div class="textHeadingDivFeedback col-lg-12">LI&Ecirc;N HỆ</div>

<ul>
	<li>
	<p><strong>Địa chỉ:</strong> 112 Phan Đ&igrave;nh Ph&ugrave;ng, Phường 6, TP. Đ&agrave; Lạt</p>
	</li>
	<li>
	<p><strong>Phone:</strong> +84 63 00000</p>
	</li>
	<li>
	<p><strong>Email:</strong><a href="mailto:pxmail@yahoo.com">pxmail@yahoo.com</a></p>
	</li>
</ul>
</div>

<div class="col-lg-3 divWrapperImageGallery">
<div class="textHeadingDivFeedback col-lg-12">THƯ VIỆN ẢNH</div>

<div id="gallery">
<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg1.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg1.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg2.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg2.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg3.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg3.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg4.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg4.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg5.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg5.jpg" /> </a></div>

<div class="imageInGallery col-sm-4 col-xs-6"><a href="/Content/FrontEnd/img/galleryImg6.jpg"><img class="img-responsive" src="/Content/FrontEnd/img/galleryImg6.jpg" /> </a></div>

<div class="clearfix visible-xs">&nbsp;</div>
</div>
</div>

<div class="clearfix">&nbsp;</div>
</div>
', NULL, N'Home', 1005, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Title
- Update field: Content
- Update field: Caption
', 0, 1, CAST(0x0000A31C01744810 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (44, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
<span>News</span></div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>', NULL, N'Tin-Tuc-Su-Kien', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
', 0, 1, CAST(0x0000A31C0185E40B AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[PageLogs] ([Id], [PageId], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [FileTemplateId], [Status], [ParentId], [IncludeInSiteNavigation], [StartPublishingDate], [EndPublishingDate], [Keywords], [ChangeLog], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (45, 7, N'Tin Tức - Sự Kiện', N'Tin tức sự kiện', NULL, N'<div class="divWrapperBigHeadingIntroText">
<div class="col-sm-11 col-sm-offset-1 bigHeadingIntroText">
<h1>Tin tức</h1>
<span>News</span></div>
</div>

<div class="divWrapperRoomBookingOnlineAndContact">
<div class="col-sm-8">{NewsListing}</div>', NULL, N'Tin-Tuc-Su-Kien', 1007, NULL, 1, NULL, 1, NULL, NULL, NULL, N'** Update Page **
- Update field: Content
- Update field: PageTemplateId
', 0, 1, CAST(0x0000A31C0186D225 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PageLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[RotatingImageGroups] ON 

GO
INSERT [dbo].[RotatingImageGroups] ([Id], [Name], [Settings], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Home', N'{"AutoPlay":3000,"StopOnHover":true,"Navigation":true,"PaginationSpeed":2000,"PaginationNumbers":true,"LazyLoad":true,"GoToFirstSpeed":2000,"SingleItem":true,"AutoHeight":true,"TransitionStyle":"fade"}', 0, 1, CAST(0x0000A302001E3253 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3020024E1E9 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[RotatingImageGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[RotatingImages] ON 

GO
INSERT [dbo].[RotatingImages] ([Id], [ImageUrl], [Text], [Url], [GroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'/Media/RotatingImages/hotelRoom1.jpg', NULL, N'#', 2, 4, 1, CAST(0x0000A30F00001AED AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31901290FC5 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[RotatingImages] ([Id], [ImageUrl], [Text], [Url], [GroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'/Media/RotatingImages/hotelRoom2.jpg', NULL, N'#', 2, 8, 1, CAST(0x0000A30F009FFCA3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31901292D88 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[RotatingImages] ([Id], [ImageUrl], [Text], [Url], [GroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'/Media/RotatingImages/hotelRoom3.jpg', NULL, N'#', 2, 10, 1, CAST(0x0000A30F00A0117F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3190129439D AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[RotatingImages] ([Id], [ImageUrl], [Text], [Url], [GroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, N'/Media/RotatingImages/hotelRoom4.jpg', NULL, N'#', 2, 11, 1, CAST(0x0000A31901295EC2 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[RotatingImages] ([Id], [ImageUrl], [Text], [Url], [GroupId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, N'/Media/RotatingImages/hotelRoom5.jpg', NULL, N'#', 2, 12, 1, CAST(0x0000A3190129612A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A3190129830C AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[RotatingImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Banners] ON 

GO
INSERT [dbo].[Banners] ([Id], [ImageUrl], [Text], [Url], [GroupName], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'/Media/Banners/bannerBg.jpg', NULL, N'#', N'Home', 0, 1, CAST(0x0000A31A001907ED AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Banners] OFF
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1, N'Dashboards', NULL, N'Home', N'Index', NULL, N'.00001.', N'icon-headphones', 1, 1, CAST(0x0000A2F3017D395F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5127F AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (2, N'Contents', NULL, NULL, NULL, NULL, N'.00002.', N'icon-comment', 1, 4, CAST(0x0000A2F3017D83A3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512A9 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (3, N'Configuration', NULL, NULL, NULL, NULL, N'.00003.', N'icon-adjust', 1, 5, CAST(0x0000A2F3017DAD77 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512AE AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (4, N'Users', NULL, NULL, NULL, NULL, N'.00004.', N'icon-adjust', 1, 7, CAST(0x0000A2F3017DEEC3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512AF AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (5, N'Bookings', NULL, NULL, NULL, NULL, N'.00005.', N'icon-adjust', 1, 5, CAST(0x0000A2F3017E07CA AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512B3 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (6, N'Site Settings', NULL, N'SiteSettings', N'Index', 1034, N'.00003.01034.00006.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EC04C AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512BC AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (7, N'Menus', NULL, NULL, NULL, 2, N'.00002.00007.', N'icon-adjust', 1, 2, CAST(0x0000A2F3017EDD21 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512C2 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (8, N'Pages', NULL, NULL, NULL, 2, N'.00002.00008.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EF5F2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512C6 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (9, N'User List', NULL, N'Users', N'Index', 4, N'.00004.00009.', N'icon-adjust', 1, 1, CAST(0x0000A2F301881DB3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512CD AS DateTime), N'levanvunam@gmail.com', N'2', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (10, N'User Groups', NULL, N'UserGroups', N'Index', 4, N'.00004.00010.', N'icon-adjust', 1, 2, CAST(0x0000A2F3018835E9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512D3 AS DateTime), N'levanvunam@gmail.com', N'2', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (11, N'Permissions', NULL, N'UserGroups', N'Permissions', 10, N'.00004.00010.00011.', N'icon-adjust', 1, 3, CAST(0x0000A2F3018889E5 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512D9 AS DateTime), N'levanvunam@gmail.com', N'2', 0)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (12, N'Languages', NULL, N'Languages', N'Index', 3, N'.00002.00012.', N'icon-adjust', 1, 3, CAST(0x0000A2F400031B82 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512DE AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (14, N'Page Templates', NULL, N'PageTemplates', N'Index', 1030, N'.00002.00008.01030.00014.', N'icon-adjust', 1, 1, CAST(0x0000A2F700347D91 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512E2 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1013, N'Testimonials', NULL, N'Testimonials', N'Index', 2, N'.00002.01013.', N'icon-adjust', 1, 4, CAST(0x0000A2FA015C56CD AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512E8 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1016, N'Setting Types', NULL, N'SettingTypes', N'Index', 1034, N'.00003.01034.01016.', N'icon-adjust', 1, 2, CAST(0x0000A2FB00023BA1 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512ED AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1017, N'News', NULL, NULL, NULL, NULL, N'.00002.01017.', N'icon-adjust', 1, 3, CAST(0x0000A2FB000DBE34 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512F0 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1018, N'News List', NULL, N'News', N'Index', 1017, N'.01017.01018.', N'icon-adjust', 1, 1, CAST(0x0000A2FB000DDAD1 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512F6 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1019, N'News Categories', NULL, N'NewsCategories', N'Index', 1017, N'.01017.01019.', N'icon-adjust', 1, 2, CAST(0x0000A2FB000DEE1D AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E512FC AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1020, N'Pages List', NULL, N'Pages', N'Index', 8, N'.00002.00008.01020.', N'icon-adjust', 1, 1, CAST(0x0000A2FB000E16C2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51302 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1021, N'Templates', NULL, N'Templates', N'Index', NULL, N'.01021.', N'icon-adjust', 1, 6, CAST(0x0000A2FC016E1E7A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51308 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1022, N'Rotating Images', NULL, NULL, NULL, 2, N'.00002.01022.', N'icon-adjust', 1, 7, CAST(0x0000A301018842B3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5130C AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1023, N'List', NULL, N'RotatingImages', N'Index', 1022, N'.00002.01022.01023.', N'icon-adjust', 1, 1, CAST(0x0000A3010188870A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51314 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1024, N'Groups', NULL, N'RotatingImageGroups', N'Index', 1022, N'.00002.01022.01024.', N'icon-adjust', 1, 2, CAST(0x0000A3010188AEB7 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5131D AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1025, N'Tags', NULL, N'Tags', N'Index', 8, N'.00002.00008.01025.', N'icon-adjust', 1, 4, CAST(0x0000A30B0154A77A AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5132A AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1026, N'Gallery', NULL, N'RotatingImageGroups', N'Gallery', 1024, N'.00002.01022.01024.01026.', N'icon-adjust', 1, 1, CAST(0x0000A30F01620832 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51334 AS DateTime), N'levanvunam@gmail.com', N'4', 0)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1027, N'Admin', NULL, N'Menus', N'Index', 7, N'.00002.00007.01027.', N'icon-adjust', 1, 1, CAST(0x0000A30F01645AE2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5133D AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1028, N'Client', NULL, N'ClientMenus', N'Index', 7, N'.00002.00007.01028.', N'icon-adjust', 1, 2, CAST(0x0000A30F0164987F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51343 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1029, N'File Templates', NULL, N'FileTemplates', N'Index', 1030, N'.00002.00008.01030.01029.', N'icon-adjust', 1, 2, CAST(0x0000A3100124F607 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51349 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1030, N'Templates', NULL, NULL, NULL, 8, N'.00002.00008.01030.', N'icon-adjust', 1, 3, CAST(0x0000A311013E68A3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5134C AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1031, N'Page Logs', NULL, N'PageLogs', N'Index', 8, N'.00002.00008.01031.', N'icon-adjust', 1, 5, CAST(0x0000A315017B95EC AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51353 AS DateTime), N'levanvunam@gmail.com', N'2', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1032, N'Tools', NULL, NULL, NULL, NULL, N'.01032.', N'icon-adjust', 1, 8, CAST(0x0000A31800E6D335 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51356 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1033, N'SQL Tool', NULL, N'SQLTool', N'Index', 1032, N'.01032.01033.', N'icon-adjust', 1, 1, CAST(0x0000A31800E6F0F8 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5135F AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1034, N'Settings', NULL, NULL, NULL, 3, N'.00003.01034.', N'icon-adjust', 1, 1, CAST(0x0000A318010AAE38 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51360 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1039, N'Banners', NULL, N'Banner', N'Index', 2, N'.00002.01039.', N'icon-adjust', 1, 6, CAST(0x0000A31A001B6660 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51367 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1040, N'Services', NULL, N'Services', N'Index', 1041, N'.01040.', N'icon-adjust', 1, 1, CAST(0x0000A31A00EC64D9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E5136E AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1041, N'Hotels', NULL, NULL, NULL, NULL, N'.01041.', N'icon-adjust', 1, 2, CAST(0x0000A31A00EC7CA1 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31F00E51372 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

GO
INSERT [dbo].[Services] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Service 1', N'Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.', N'<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</p>
', N'/Media/Services/service1.jpg', 1, 0, 1, CAST(0x0000A31A00EF2713 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A00F021B1 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Services] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Service 2', N'Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.', N'<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</p>
', N'/Media/Services/service2.jpg', 1, 0, 1, CAST(0x0000A31A00F0F878 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[Services] ([Id], [Title], [Description], [Content], [ImageUrl], [Status], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Service 3', N'Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.', N'<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.</p>
', N'/Media/Services/service3.jpg', 1, 0, 1, CAST(0x0000A31A00F11C45 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A31A00F12BE1 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Testimonials] ON 

GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Nam Le', N'Chairman of XXX.com', N'', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FA015506A0 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6E70 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Liem Nguyen', N'Chairman of xvideos.com', N'/Media/Testimonials/testimonialImg1.jpg', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Kha Nguyen', N'Chairman of xvideos.net', N'/Media/Testimonials/testimonialImg2.jpg', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Khoa Nguyen', N'Chairman of xvideos.xxx', NULL, N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Liem Nguyen', N'Chairman of xvideos.com', N'/Media/Testimonials/testimonialImg1.jpg', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'Liem Nguyen', N'Chairman of xvideos.com', N'/Media/Testimonials/testimonialImg2.jpg', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'Liem Nguyen', N'Chairman of xvideos.com', NULL, N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Testimonials] ([Id], [Author], [AuthorDescription], [AuthorImageUrl], [Content], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'Liem Nguyen', N'Chairman of xvideos.com', N'/Media/Testimonials/testimonialImg1.jpg', N'Những ngày lưu lại ở PX đã thực sự làm cho chúng tôi hài lòng. Khách sạn rất sạch sẽ, tiện nghi, buffet sáng rất ngon, nhân viên nhẹ nhàng, nhiệt tình. Nếu có dịp quay lại Đà Lạt, tôi sẽ lại chọn PX Hotel!', 0, 1, CAST(0x0000A2FD01623587 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A319012E6148 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Testimonials] OFF
GO
