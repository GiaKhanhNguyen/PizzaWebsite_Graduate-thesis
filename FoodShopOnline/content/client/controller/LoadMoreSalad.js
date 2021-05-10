$(function () {

    var loadCount = 1;
    var loading = $("#loading");
    $("#LoadMoreSalad").on("click", function (e) {

        e.preventDefault();

        $(document).on({

            ajaxStart: function () {
                loading.show();
            },
            ajaxStop: function () {
                loading.hide();
            }
        });

        var url = "/Home/LoadMoreSalad/";
        $.ajax({
            url: url,
            data: { size: loadCount * 4 },
            cache: false,
            type: "POST",
            success: function (data) {

                if (data.length !== 0) {
                    $(data.ModelString).insertBefore("#LoadMoreSalad").hide().fadeIn(2000);
                }

                var ajaxModelCount = data.ModelCount - (loadCount * 4);
                if (ajaxModelCount <= 0) {
                    $("#LoadMoreSalad").hide().fadeOut(2000);
                }

            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        loadCount = loadCount + 1;
    });
});
