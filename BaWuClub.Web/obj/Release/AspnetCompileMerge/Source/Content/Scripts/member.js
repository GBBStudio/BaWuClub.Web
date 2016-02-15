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

    //样式相关
    function togglemassbox() {
        $(".mass-box").slideToggle();
    }


    //验证码
    function getverfycode() {
        var phone = $("#phone").val();
        if ($("#phone").val().length != 0) {
            if (/^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(phone)) {
                $(".btn-verfy").attr("disabled", "disabled");
                gettimeleft(60)
                sendmsg()
            } else {
                settips("手机号码不正确！");
            }
        } else {
            settips("手机号码不能为空！");
        }
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

    //弹窗
    function popup(data) {
        appendmaskwrap()
        $(document.body).append(data).hide().fadeIn(500);
    }

    function appendmaskwrap() {
        $(document.body).append("<div class=\"mask-wrap\"></div>");
    }

    //功能操作
    function setread(e, id) {
        $(e).removeClass("unread")
        $(e).parent("li").children("p").slideToggle();
        setget("/member/setmsgread", { "id": id }, setreadcallback);
    }

    function setendtask(param) {
        setget("/forum/setendtask", { "id": param }, setendtaskcallback)
    }

    function delmsg(e, id) {
        setget("/member/setmsgdel", { "id": id }, setdelcallback);
        $(e).parent("li").remove();
    }

    function getanswerlist(id,page) {
        setget("/ask/getanswser", { "qid": id,"page":page }, setdelcallback);
    }

    function setanswerlist(data) {
        if (data.status == "warning") {
            redirectUrl(data.url);
        } else {
            if (data.status = "success") {
                alert(data.context)
            } else
                errortips();
        }
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

    function setendtaskcallback(data) {
        if (data.status == "warning") {
            redirectUrl(data.url);
        } else {
            if (data.status = "success") {
                settips("操作成功！");
                setTimeout(clearTips,2500,document.getElementById("tips-wrap"))
               // alert("消息删除成功！");
            }
            else
                errortips();
        }
    }
   

    //列表相关
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

    //验证相关
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


    //提示相关

    function settips(text) {
        $("#tips-wrap").html("<span>" + text + "</span>");
    }

    function clearTips(wrap) {
        wrap.innerHTML = "";
    }

    function errortips() {
        alert("操作异常，请稍后重试！");
    }

    function redirectUrl(url) {
        window.location.href = url;
    }

    function setwrapcenter(element) {
        var width = element.offsetWidth
        var height = element.offsetHeight
        var top = (window.document.body.clientHeight - height) / 2
        var left = (document.body.clientWidth - width) / 2
        element.setAttribute("style", "top:" + top + "px;left:" + left + "px")
    }
