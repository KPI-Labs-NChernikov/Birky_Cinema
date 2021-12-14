let $recommendedSlider = $('#carousel-1');
let $recommended = $('#carousel-1 .filmBlock');
let $recommendedArrowLeft = $('#triangleLeft');
let $recommendedArrowRight = $('#triangleRight');
let recommended = {slider: $recommendedSlider, elements: $recommended, arrowLeft: $recommendedArrowLeft,
     arrowRight: $recommendedArrowRight};

let carousels = [recommended];

$(function() {
    addCarouselsListeners(carousels);
});