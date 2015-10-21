$(function () {
    $(".member-menu a").each(function(index){
        var _this = $(this);
        _this.click(function () {
            if (_this.hasClass("selected"))
                return;
            else {
                _this.addClass("selected");
                _this.siblings().removeClass("selected");
                var _e = _this.attr("data-dsk");
                $("." + _e).show();
                $("." + _e).siblings("div").hide();
            }
        });
    });

    $(document).on("click", ".page-wrap a", function () {
        $(".member-list-wrap ul").html("<img src=\"/content/images/loading.gif\" alt=\"\"/>");
        $.get($(this).attr("data-href"), function (data) {
            var json = eval(data);
            $(".page-wrap").html(json.pagestr);
            console.log(1);
            var items = eval(json.context);
            var str = "";
            for (var i = 0; i < items.length; i++) {
                    str+= "<li><a href='" + json["url"] + items[i]["Id"] + "'>" + items[i]["Title"] + "</a><span>" + items[i]["VarDate"] + "</span></li>";
                $(".member-list-wrap ul").html(str);
            }
        }, "json");
    });
})

function checkSubmit(e) {
    var inputs = e.getElementsByTagName("input");
    var tips = document.getElementById("tips-wrap");
    for (var i = 0; i < inputs.length; i++) {
        var input = inputs[i];
        if (input.dataset.required == "true") {
            if (input.value.length == 0) {
                tips.innerHTML = "<span>" + input.dataset.hint + "</span>";
                return false;
            }
        }
    }

    $.post($(e).attr("action"), $(e).serialize(), function (result) {
        var data = eval(result);
        if (data.state == "success") {
            if (data.url)
                setTimeout(redirectUrl, 600, data.url);
            if (data.src)
                $(".member-cover img").attr("src","/uploads/avatar/big/"+data.src);

        }
        tips.innerHTML = "<span>" + data.context + "</span>";
        setTimeout(clearTips, 1000,tips);
    }, "json");
    return false;
}

function clearTips(wrap) {
    wrap.innerHTML = "";
}

function redirectUrl(url) {
    window.location.href = url;
}

