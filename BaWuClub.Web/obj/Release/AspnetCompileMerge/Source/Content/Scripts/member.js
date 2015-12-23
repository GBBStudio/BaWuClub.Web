﻿$(function () {
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
        loadonpage($(this).attr("data-href"));
    });

    $(document).on("click", ".message-close", function () {
        $(".message-wrap").fadeOut(200,function () {
            $(this).remove();
        });
        $(".message-mask").fadeOut(200,function () {
            $(this).remove();
        });
    });
})

function SendMessage(params) {
    $.get('getmssageview', {"uid":params}, function (data) {
        $(document.body).append("<div class=\"message-mask\"></div>");
        $(document.body).append(data).hide().fadeIn(500);
    }, "html");
}

function loadonpage(url) {
    $(".member-list-wrap ul").html("<img src=\"/content/images/loading.gif\" alt=\"\"/>");
    $.get(url, function (data) {
        var json = eval(data);
        $(".page-wrap").html(json.pagestr);
        var items = eval(json.context);
        if (json["url"] != "/message/show/")
            setgenerallist(json, json["url"]);
        else
            setmessagelist(items);
    }, "json");
}

function setread(e, id) {
    $(e).removeClass("unread")
    $(e).parent("li").children("p").slideToggle();
    setget("/member/setmsgread", { "id": id }, setreadcallback);
}

function delmsg(e, id) {
    setget("/member/setmsgdel", { "id": id }, setdelcallback);
    $(e).parent("li").remove();
}

function setreadcallback(data) {
    if (data.status == "warning") {
        redirectUrl(data.url);
    } else {
        if (data.status = "success") {
        } else errortips();
    }
}

function setdelcallback(data) {
    if (data.status == "warning") {
        redirectUrl(data.url);
    } else {
        if (data.status = "success") {
            alert("消息删除成功！");
        }
        else
          errortips();
    }
}

function setget(url, params,callback) {
    $.get(url, params, function (data) {
        var json = eval(data);
        callback(json);
    }, "json");
}

function setmessagelist(items) {
    var str = "";
    for (var i = 0; i < items.length; i++) {
        str += "<li><a class=\"" + (items[i]["Status"] == "0" ? "unread" : "") + "\" onclick=\"setread(this," + items[i]["Id"] + ")\">" + items[i]["Title"] + "</a><span onclick=\"delmsg(e," + items[i]["Id"] + ")\">" + items[i]["VarDate"] + "</span><span onclick=\"if(confirm('确定删除此信息吗？')){delmsg(this," + items[i]["Id"] + ")}\" class=\"message-del-btn fright\">删除</span><p style=\"display:none\">" + items[i]["Message1"] + "</p></li>";
        $(".member-list-wrap ul").html(str);
    }
}

function setgenerallist(d,urlparam) {
    var str = "";
    items=eval(d.context);
    if (items.length == 0) {
        $(".member-list-wrap ul").html("<span style=\"font-size:15px;color:#007ADA\">暂无数据</span>");
    } else {
        for (var i = 0; i < items.length; i++) {
            var state = "", edit = "";
            //console.log(d.StateShow)
            if (items[i]["Status"] != undefined && d.StateShow) {
                state = "<span class=\"c-" + (parseInt(items[i]["Status"]) > 0 ? "enable" : "disable") + "\">" + (parseInt(items[i]["Status"]) > 0 ? "已审核" : "待审核")+ "</span>";
            }
            if (d["edit"] != undefined && d["edit"]) {
                edit = "<a href=\""+d["editurl"]+""+items[i]["Id"]+"\" class=\"edit-again\">再次编辑</a>";
            }
            str += "<li><a href='" + urlparam + items[i]["Id"] + "'>" + items[i]["Title"] + "</a><span>" + items[i]["VarDate"] + "</span>" + state + edit + "</li>";
            $(".member-list-wrap ul").html(str);
        }            
    }
}

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

function errortips(){
    alert("操作异常，请稍后重试！");
}