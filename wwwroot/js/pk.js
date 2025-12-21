$(document).ready(function(){
  $('.sharing-slider').slick({
    slidesToShow: 3,      // hiển thị 5 ảnh cùng lúc
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 2000,
    arrows: true,
    dots: false,
    infinite: true,
    responsive: [
      { breakpoint: 1024, settings: { slidesToShow: 4 } },
      { breakpoint: 768,  settings: { slidesToShow: 2 } },
      { breakpoint: 480,  settings: { slidesToShow: 1 } }
    ]
  });
});
