
$(document).ready(function(){$(".footer").append('<a class="backtotop" href="#0"><i class="fa fa-chevron-up"></i></a>');var o=300,a=1200,s=700,l=$(".backtotop");$(window).scroll(function(){$(this).scrollTop()>o?l.addClass("is-visible"):l.removeClass("is-visible fade-out"),$(this).scrollTop()>a&&l.addClass("fade-out")}),l.on("click",function(o){o.preventDefault(),$("body,html").animate({scrollTop:0},s)})});
$(document).ready(function(){$("#icon-menu").click(function(){$(".mainmenu").toggleClass("open"),$(".page").toggleClass("menu-open"),$(".overlay").toggleClass("show-overlay")}),$(".overlay").click(function(){$(".mainmenu").hasClass("open")&&($(".mainmenu").removeClass("open"),$(".page").hasClass("menu-open")&&$(".page").removeClass("menu-open"),$(".overlay").hasClass("show-overlay")&&$(".overlay").removeClass("show-overlay"))})}),$(document).ready(function(){$(window).on("scroll",function(){$topOffset=$(this).scrollTop(),console.log($topOffset),$topOffset>200?$(".col-top.afixed").addClass("small-menu"):$(".col-top.afixed").removeClass("small-menu")})});