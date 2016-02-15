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
        loadonpage($(this).attr("data-href"));
    });

    $(document).on("click", ".close-btn", function () {
        var t=$(this).parent().parent()
        t.fadeOut(200, function () {
            $(this).remove();
        });
        $(".mask-wrap").fadeOut(200, function () {
            $(this).remove();
        });
    });
})

function getmoreopreate(parameters) {
    var element = document.createElement("div")
    var left = (document.body.clientWidth - 1022) / 2
    element.setAttribute("class", "topic-moreset-wrap")
    element.setAttribute("Style", "left:" + left + "px")
    element.innerHTML = "<img src='/content/images/loading.gif'/>"
    popup(element);
    $.get('getmoreset', { "id": parameters }, function (data) {
        element.innerHTML = data
    }, "html");
}

function togglemassbox() {
    $(".mass-box").slideToggle();
}

function getverfycode() {
    var phone = $("#phone").val();
    if ($("#phone").val().length != 0) {        
        if (/^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(phone)) {
            $(".btn-verfy").attr("disabled", "disabled");
            gettimeleft(60)
            sendmsg()
        }else{
            settips("手机号码不正确！");
        }
    }else{
        settips("手机号码不能为空！");
    }
}

    function settips(text){
        $("#tips-wrap").html("<span>"+text+"</span>");
    }

    function back() {
        $(".btn-verfy").removeAttr("disabled")
        $(".btn-verfy").val("获取验证码");
    }

    function gettimeleft(i) {
        $(".btn-verfy").val("请等待" + i + "秒");
        if (i > 0) {
            setTimeout(gettimeleft, 1000, i - 1)
        } else {
            back()
        }
    }

    function sendmsg() {
        $.post("/member/SetSMSVerifyCode", { "phone": $("#phone").val() }, function (json) {
            var data = eval(json);
            $("#tips-wrap").html("<span>" + data.context + "</span>");
        }, "json")
    }

    function SendMessage(params) {
        $.get('getmssageview', { "uid": params }, function (data) {
            popup(data);
        }, "html");
    }

    function popup(data) {
        appendmaskwrap()
        $(document.body).append(data).hide().fadeIn(500);
    }

    function appendmaskwrap() {
        $(document.body).append("<div class=\"mask-wrap\"></div>");
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
                var state = "", edit = "",more="";
                if (items[i]["Status"] != undefined && d.StateShow) {
                    state = "<span class=\"c-" + (parseInt(items[i]["Status"]) > 0 ? "enable" : "disable") + "\">" + (parseInt(items[i]["Status"]) > 0 ? "已审核" : "待审核")+ "</span>";
                }
                if (d["edit"] != undefined && d["edit"]) {
                    edit = "<a href=\""+d["editurl"]+""+items[i]["Id"]+"\" class=\"edit-again\">再次编辑</a>";
                }
                if (d["more"] != undefined && d["more"])
                    more = "<a onclick=\"getmoreopreate("+items[i]["Id"] + ")\" class=\"edit-again\">更多操作</a>";
                str += "<li><a href='" + urlparam + items[i]["Id"] + "'>" + items[i]["Title"] + "</a><span>" + items[i]["VarDate"] + "</span>" + state + edit + more + "</li>";
                $(".member-list-wrap ul").html(str);
            }            
        }
    }

    function checksubmitverify(e) {
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
        return true;
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

    function setwrapcenter(element) {
        var width = element.offsetWidth
        var height = element.offsetHeight
        var top = (window.document.body.clientHeight - height) / 2
        var left = (document.body.clientWidth - width) / 2
        element.setAttribute("style", "top:" + top + "px;left:" + left + "px")
    }

    function sendmassmsg(e) {
        var tips=document.getElementById("tips-wrap")
        if ($("#context").val().length == 0) {
            settips("要发送的信息不能为空！");
            return false;
        }
        $.post("/message/masssend", {"ids":$(e).attr("data-users"),"context":$("#context").val()}, function (result) {
            var data = eval(result);
            if (data.status == "success") {
                settips(data.context);
                $(".mass-box").slideUp()
                setTimeout(function () { $("#context").val("") }, 1000)
            } else {
                settips(data.context);
                $(".mass-box").slideUp()
                setTimeout(function () { $("#context").val("") }, 1000)
            }
            setTimeout(clearTips, 4000, tips);
        }, "json");
    }