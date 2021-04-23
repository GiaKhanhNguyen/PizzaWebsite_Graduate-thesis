var payment = {
    init: function () {
        payment.regEvents();
    },
    regEvents: function () {
        $('#btnComeback').off('click').on('click', function (e) {
            window.location.href = "/hinh-thuc-dat-hang";
        });
        $('#btnbackIOrder').off('click').on('click', function (e) {
            window.location.href = "/thong-tin-dat-hang";
        });
        $('#btnComebackCart').off('click').on('click', function (e) {
            window.location.href = "/gio-hang";
        });
    }
}
payment.init();
