autosize($('#new-review-text'));

let $recommendedSlider = $('#carousel');
let $recommended = $('#carousel .filmBlock');
let $recommendedArrowLeft = $('#triangleLeft');
let $recommendedArrowRight = $('#triangleRight');
let recommended = {slider: $recommendedSlider, elements: $recommended, arrowLeft: $recommendedArrowLeft,
     arrowRight: $recommendedArrowRight};

let carousels = [recommended];

$(function() {
    addCarouselsListeners(carousels);
});

var maxLength = 400;
$('textarea').keyup(function() {
  $('#rchars').text($(this).val().length + "/" + maxLength);
});