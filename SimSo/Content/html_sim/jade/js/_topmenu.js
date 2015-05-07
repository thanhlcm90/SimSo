$(document).ready(function(){
    //top-menu
    $('#icon-menu').click(function(){ 
      $(".mainmenu").toggleClass("open");
      $(".page").toggleClass("menu-open");  
      $('.overlay').toggleClass('show-overlay');
    });
    $('.overlay').click(function(){
    if( $('.mainmenu').hasClass('open') ) {
     $(".mainmenu").removeClass("open");
      if( $('.page').hasClass('menu-open') ) {
        $(".page").removeClass("menu-open");
      }
      if($('.overlay').hasClass('show-overlay')){
        $('.overlay').removeClass('show-overlay');
      }
    }
  });
 
}); 

$(document).ready(function(){
  $(window).on('scroll',function(){
    $topOffset = $(this).scrollTop();
    console.log($topOffset);
    if ($topOffset > 200){
      $('.col-top.afixed').addClass('small-menu');
    }
    else {
       $('.col-top.afixed').removeClass('small-menu');
    }
  });
});