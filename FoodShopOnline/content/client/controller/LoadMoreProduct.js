$(function () {

    var loadCount = 1;
     var loading = $("#loading");
    $("#loadMore").on("click", function (e) {

        e.preventDefault();

        $(document).on({

            ajaxStart: function () {
                loading.show();
            },
            ajaxStop: function () {
                loading.hide();
            }
        });

        var url = "/Home/LoadMoreProduct/";
        $.ajax({
            url: url,
            data: { size: loadCount * 4 },
            cache: false,
            type: "POST",
            success: function (data) {

                if (data.length !== 0) {
                    $(data.ModelString).insertBefore("#loadMore").hide().fadeIn(2000);
                }

                var ajaxModelCount = data.ModelCount - (loadCount * 4);
                if (ajaxModelCount <= 0) {
                    $("#loadMore").hide().fadeOut(2000);
                }

            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        loadCount = loadCount + 1;
    });
});