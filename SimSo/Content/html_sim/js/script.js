




// backtotop.js
$(document).ready(function() {
  $('body').append('<a class="backtotop" href="#0"><i class="fa fa-chevron-up"></i></a>');   
  // browser window scroll (in pixels) after which the "back to top" link is shown
  var offset = 300,
    //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
    offset_opacity = 1200,
    //duration of the top scrolling animation (in ms)
    scroll_top_duration = 700,
    //grab the "back to top" link
    $back_to_top = $('.backtotop');

  //hide or show the "back to top" link
  $(window).scroll(function(){
    ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('is-visible') : $back_to_top.removeClass('is-visible fade-out');
    if( $(this).scrollTop() > offset_opacity ) { 
      $back_to_top.addClass('fade-out');
    }
  });

  //smooth scroll to top
  $back_to_top.on('click', function(event){
    event.preventDefault();
    $('body,html').animate({
      scrollTop: 0 ,
      }, scroll_top_duration 
    );
  });

});
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
    if ($topOffset > 100){
      $('.col-top.afixed').addClass('small-menu');
    }
    else {
       $('.col-top.afixed').removeClass('small-menu');
    }
  });
});
// check order.js
$(function(){
      //- độ dài lớn nhất và nhỏ nhất của số 
      var max_length_number    = 12;
      var min_length_number    = 9;          


      var set_number_infor     = $(".set-number-infor");

      var phonenumber           = $("input.phonenumber");
      var setnumber             = $(".set-number");
      var set_price             = $(".phone-price");
      var set_telco             = $(".set-telco");

      var label_info            = $(".label-numberinfo");
      var label_guide           = $('.label-guide'); 

      //- set label guide
      set_label_guide = function(){
        var value                 = setnumber.text();
        var value_number   = value.replace(/\s+/g, '');  
        if (value == ""){        
           set_number_infor.text(""+ value);
        }
        else{
          if ($.isNumeric(value_number)) {
            set_number_infor.text(value);
          }        
        } 
      }
      set_label_guide();
      

      // fill phone number
      phonenumber.change(function(){
        var value           = $(this).val(); 
        value               = value.replace(/\s+/g, '');

        if ($.isNumeric(value)) {
          console.log(value.length);
          if((value.length>=min_length_number)&(value.length<=max_length_number)){
            //- console.log("longer than 9");
            setnumber.text(value);
            set_price.text("Giá trong CSDL");
          }
        else {
            setnumber.text("Không xác định");
            set_price.text("Giá ko xác định");
          }
        }
        else {
          //- console.log("not a number");
          }

        set_label_guide();
        });

      // Toggle guide_content
      label_info.click(function(event){
        event.preventDefault();
        label_guide.slideToggle();
        });

    });