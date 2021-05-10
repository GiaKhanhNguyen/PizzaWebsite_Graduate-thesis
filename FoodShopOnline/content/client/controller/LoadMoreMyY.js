$(function () {

    var loadCount = 1;
    var loading = $("#loading");
    $("#LoadMoreMyY").on("click", function (e) {

        e.preventDefault();

        $(document).on({

            ajaxStart: function () {
                loading.show();
            },
            ajaxStop: function () {
                loading.hide();
            }
        });

        var url = "/Home/LoadMoreMyY/";
        $.ajax({
            url: url,
            data: { size: loadCount * 4 },
            cache: false,
            type: "POST",
            success: function (data) {

                if (data.length !== 0) {
                    $(data.ModelString).insertBefore("#LoadMoreMyY").hide().fadeIn(2000);
                }

                var ajaxModelCount = data.ModelCount - (loadCount * 4);
                if (ajaxModelCount <= 0) {
                    $("#LoadMoreMyY").hide().fadeOut(2000);
                }

            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        loadCount = loadCount + 1;
    });
});
