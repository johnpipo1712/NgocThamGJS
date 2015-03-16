jQuery(document).ready(function(){
	$('#backTop').hide();
	$(window).scroll(function () {
		if ($(this).scrollTop() > 100) {
			$('#backTop').fadeIn(100);
		} else {
			$('#backTop').fadeOut();
		}
	});
	$('#backTop').click(function () {
		$('body,html').animate({
			scrollTop: 0
		}, 800);
		return false;
	});
	$('.zoom').fancybox();
	$("a[rel='imgGroup']").fancybox({
    	openEffect	: 'fade',
    	closeEffect	: 'fade',
		autoPlay   : false,
		playSpeed  : 6000,
		prevEffect		: 'none',
		nextEffect		: 'none',
    });
	//nums
	$("#nums #listNums").click(function(){
		$("#nums #listNums div").slideToggle(0);
	});
	$(".clickShow").click(function(){
		$("#comment").addClass("showComment");
	});
	$(".clickHide").click(function(){
		$("#comment").removeClass("showComment");
	});
	//video
	$(".oneVideo").live('click',function(){
		var idVideo = $(this).attr("id");
		$.get("video-load.php",{id:idVideo},function(data){
			$("#showVideo").html(data);
		});
		$(".oneVideo").removeClass("active");
		$(this).addClass("active");
		$('body,html').animate({
			scrollTop: $('#colLeft').offset().top
		}, 200);
	});
	$("#menuMobile h1").click(function(){
		$(this).next("ul").slideToggle(300)
		.siblings("ul:visible").slideUp(300);
		$(this).toggleClass("active");
		$(this).siblings("h1").removeClass("active");
	});
	$("#menuMobile h2").click(function(){
		$(this).next("ul").slideToggle(300)
		.siblings("ul:visible").slideUp(300);
		$(this).toggleClass("active");
		$(this).siblings("h2").removeClass("active");
	});
	$('#colLeft #showText img').each(function() {
		var url = $(this).attr('src');
		var title = $(this).attr('alt');
		$(this).after('<a href=' + url +' data-gallery="" class="data-gallery" title="' + title + '"><img src="' + url + '" /></a>').css({display:'none'});
	});
});
this.sitemapstyler = function(){
	var sitemap = document.getElementById("sitemap")
	if(sitemap){
		
		this.listItem = function(li){
			if(li.getElementsByTagName("ul").length > 0){
				var ul = li.getElementsByTagName("ul")[0];
				ul.style.display = "none";
				var span = document.createElement("span");
				span.className = "collapsed";
				span.onclick = function(){
					ul.style.display = (ul.style.display == "none") ? "block" : "none";
					this.className = (ul.style.display == "none") ? "collapsed" : "expanded";
				};
				li.appendChild(span);
			};
		};
		
		var items = sitemap.getElementsByTagName("li");
		for(var i=0;i<items.length;i++){
			listItem(items[i]);
		};
		
	};	
};
window.onload = sitemapstyler;