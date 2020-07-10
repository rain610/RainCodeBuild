$(function () {
    $.extend({
        min: function (a, b) {
            return a < b ? a : b;
        },
        max: function (a, b) {
            return a > b ? a : b;
        }
    })
})