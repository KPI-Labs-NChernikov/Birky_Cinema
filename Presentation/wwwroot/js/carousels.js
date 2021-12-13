let lastSegmentsNumber;
let carouselGapSize = 4;

var isPhone = window.matchMedia("(max-width: 650px)");
var isTablet = window.matchMedia("(max-width: 1200px)");
var isNotWide = window.matchMedia("(max-width: 1400px)");
var isUltrawide = window.matchMedia("(min-width: 1801px)");

function getCarouselSegmentsNumber() {
    let number = 6;
    if (isPhone.matches)
        number = 1;
    else if (isTablet.matches)
        number = 3;
    else if (isNotWide.matches)
        number = 5;
    else if (isUltrawide.matches)
        number = 8;
    return number;
}

function changeCarousels(carousels) {
    if (lastSegmentsNumber != getCarouselSegmentsNumber())
    {
        lastSegmentsNumber = getCarouselSegmentsNumber();
        for (let carousel of carousels)
        {
            carousel.arrowRight.removeClass('disabled');
            carousel.current = 0;
            carousel.arrowLeft.addClass('disabled');
            if (carousel.elements.length <= lastSegmentsNumber)
            {
                carousel.slider.css({'justify-content' : 'center'});
                carousel.arrowRight.addClass('disabled');
            }
            else 
            {
                carousel.slider.css({'justify-content' : 'start'});
            }
            carousel.slider.css({'margin-left' : '0'});
        }
    }
}

function getPrevCarouselNumber(current, length) {
    return current < 1 ? length - 1 : current - 1;
}

function scrollCarouselRight(carousel) {
    if (carousel.lock)
        return;
    else if (carousel.current + getCarouselSegmentsNumber() > carousel.elements.length - 1)
        return;
    carousel.lock = true;
    if (carousel.current == 0)
        carousel.arrowLeft.removeClass('disabled');
    if (carousel.current + getCarouselSegmentsNumber() == carousel.elements.length - 1)
        carousel.arrowRight.addClass('disabled');
    let animationSpeed = 1000;  
    carousel.slider.animate({'margin-left': '-=' + (carousel.elements.eq(carousel.current).width() + carouselGapSize) + 'px'}, animationSpeed, function() {
        carousel.current++;
        carousel.lock = false;
    });
}

function scrollCarouselLeft(carousel) {
    if (carousel.lock)
        return;
    else if (carousel.current < 1)
        return;
    carousel.lock = true;
    if (carousel.current + getCarouselSegmentsNumber() == carousel.elements.length)
        carousel.arrowRight.removeClass('disabled');
    if (carousel.current == 1)
        carousel.arrowLeft.addClass('disabled');
    let animationSpeed = 1000;  
    carousel.slider.animate({'margin-left': '+=' + (carousel.elements.eq(carousel.current).width() + carouselGapSize) + 'px'}, animationSpeed, function() {
        carousel.current--;
        carousel.lock = false;
    });
}

function addCarouselsListeners(carousels) {
    for (let carousel of carousels) 
    {
        carousel.lock = false;
        carousel.current = 0;
    };
    window.addEventListener('resize', () => {changeCarousels(carousels);});
    window.addEventListener("orientationchange",  () => {changeCarousels(carousels);});
    changeCarousels(carousels);
    for (let carousel of carousels)
    {
        carousel.arrowLeft.click(() => {scrollCarouselLeft(carousel);});
        carousel.arrowRight.click(() => {scrollCarouselRight(carousel);});
    };
}