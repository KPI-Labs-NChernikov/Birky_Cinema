'use strict';

let $posters = $(".poster");
let $postersSlider = $('.posters');
let currentPosterNumber = 0;
let posterLock = false;

function getPrevPosterNumber() {
    return currentPosterNumber < 1 ? $posters.length - 1 : currentPosterNumber - 1;
}

function getNextPosterNumber() {
    return currentPosterNumber > $posters.length - 2 ? 0 : currentPosterNumber + 1;
}

function scrollPosterRight() {
    if (posterLock)
        return;
    posterLock = true;
    let animationSpeed = 1000;  
    if (currentPosterNumber == $posters.length - 1)  
    {
        let $clone = $posters.eq(0).clone();
        $clone.appendTo($postersSlider);
        $clone.addClass('active');
        $postersSlider.animate({'margin-left': '-=100vw'}, animationSpeed, function() {
            $postersSlider.css({'margin-left': '0'});
            $posters.eq(currentPosterNumber).removeClass('active');
            $posters.eq(getNextPosterNumber()).addClass('active');
            $clone.removeClass('active');
            $clone.remove();
            currentPosterNumber = getNextPosterNumber();
            posterLock = false;
        });
    }
    else {
        $posters.eq(getNextPosterNumber()).addClass('active');
        $postersSlider.animate({'margin-left': '-=100vw'}, animationSpeed, function() {
            $postersSlider.css({'margin-left': '0'});
            $posters.eq(currentPosterNumber).removeClass('active');
            currentPosterNumber = getNextPosterNumber();
            posterLock = false;
        });
    }
}

function scrollPosterLeft() {
    if (posterLock)
        return;
    posterLock = true;
    let animationSpeed = 1000; 
    if (currentPosterNumber == 0)   
    {
        let $clone = $posters.eq($posters.length - 1).clone();
        $clone.prependTo($postersSlider);
        $clone.addClass('active');
        $postersSlider.css({'margin-left': '-=100vw'});
        $postersSlider.animate({'margin-left': '+=100vw'}, animationSpeed, function() {
            $postersSlider.css({'margin-left': '0'});
            $posters.eq(currentPosterNumber).removeClass('active');
            $posters.eq(getPrevPosterNumber()).addClass('active');
            $clone.removeClass('active');
            $clone.remove();
            currentPosterNumber = getPrevPosterNumber();
            posterLock = false;
        });
    }
    else
    {
        $posters.eq(getPrevPosterNumber()).addClass('active');
        $postersSlider.css({'margin-left': '-=100vw'});
        $postersSlider.animate({'margin-left': '+=100vw'}, animationSpeed, function() {
            $postersSlider.css({'margin-left': '0'});
            $posters.eq(currentPosterNumber).removeClass('active');
            currentPosterNumber = getPrevPosterNumber();
            posterLock = false;
        });
    }
}

function isMoreThanAHalfOfPosterInViewport() {
    const rect = $postersSlider.get(0).getBoundingClientRect();
    const windowHeight = (window.innerHeight || document.documentElement.clientHeight);
    let isInViewport = (rect.bottom > windowHeight / 2);
    return isInViewport;
}

function changePosterOnInterval() {
    const delay = 20000;
    var timerId = setInterval(function() {
        if (isMoreThanAHalfOfPosterInViewport())
            scrollPosterRight();     
    }, delay);
}

let $newMoviesSlider = $('#carousel-1');
let $newMovies = $('#carousel-1 .filmBlock');
let $newMoviesArrowLeft = $('#triangleLeft2');
let $newMoviesArrowRight = $('#triangleRight2');
let newMoviesCarousel = {slider: $newMoviesSlider, elements: $newMovies, arrowLeft: $newMoviesArrowLeft, 
    arrowRight: $newMoviesArrowRight};

let $shortsSlider = $('#carousel-2');
let $shorts = $('#carousel-2 .filmBlock');
let $shortsArrowLeft = $('#triangleLeft3');
let $shortsArrowRight = $('#triangleRight3');
let shortsCarousel = {slider: $shortsSlider, elements: $shorts, arrowLeft: $shortsArrowLeft,
     arrowRight: $shortsArrowRight};

let carousels = [newMoviesCarousel, shortsCarousel];

$(function() {
    changePosterOnInterval();
    addCarouselsListeners(carousels);
});
