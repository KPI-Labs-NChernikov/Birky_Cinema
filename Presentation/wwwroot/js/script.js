let columns = $(".lang-col");
let form = $("#languages");
columns.click(function (event) {
    const delay = 10;
    var timerId = setTimeout(function () {
        form.submit();
    }, delay);
})