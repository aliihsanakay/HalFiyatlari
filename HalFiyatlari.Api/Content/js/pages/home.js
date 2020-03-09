(function ($) {
    "use strict";

    /* Card Slider - Swiper */
    let homeSlider = new Swiper('.home-slider', {
        autoplay: {
            delay: 4000,
            disableOnInteraction: false
        },
        loop: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev'
        }
    });
})(jQuery);