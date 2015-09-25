///setting menus
$(function () {
    $(".menu-s-title").each(function () {
        var _this = $(this);
        _this.click(function () {
            if (_this.next("div").hasClass("show")) {
                _this.next("div").removeClass("show").animate("slow")
                _this.children("i").removeClass("icon-xiangxia")
                _this.children("i").addClass("icon-xiangshang")
            }
            else {
                _this.next("div").addClass("show").animate("slow")
                _this.children("i").removeClass("icon-xiangshang")
                _this.children("i").addClass("icon-xiangxia")
            }
        });
    });

    $(".comm-btns-wrap a.btn").each(function () {
        var _this = $(this);
        _this.click(function () {
            if (!ischecked("chk")) {
                setstatus("未选中行");
                setTimeout(reload, 1000);
            }
            else {
                $.post(_this.attr("data-url"), $("#form").serializeArray(), function (json) {
                    var data = eval(json);
                    if (data.state == "success") {
                        setstatus(data.context);
                        resetform();
                        setTimeout(reload, 1000);
                    }
                }, "json");
            }
        });
    });
})



function ajaxdelete(url, id) {
    if (confirm("确定要删除？")) {
        $.post("/bwum" + url, { "id": id }, function (json) {
            var data = eval(json);
            setstatus(data.content);
            setTimeout(reload, 5);
        }, "json");
    }
}

/*设置输出操作提示消息在ID为notic-wrap的DIV中显示*/
function setstatus(context) {
    $("#notic-wrap").html("<span>"+context+"</span>");
}

/*检查是否选中checkbox*/
function ischecked(name) {
    var chks = document.getElementsByName(name);
    for (var i = 0; i < chks.length; i++)
        if (chks[i].checked)
            return true;
}

/*全部选中*/
function cheakall(obj) {
    if (obj.checked)
        checkselectall(true, "chk");
    else
        checkselectall(false, "chk");
}

function checkselectall(state,name) {        
    var chks = document.getElementsByName(name);        
    for (var i = 0; i < chks.length; i++) {
        chks[i].checked = state;
    }        
}


/*重置form状态*/
function resetform() {
    document.form.reset();
}

/*页面加载控制开始*/
function reload() {
window.location.reload();
}
//控制hash页面的刷新
window.onload = function () {
    setFrameDocument();
}

//控制redirect hash page
window.onhashchange = function () {
    setFrameDocument();
}

///get hash Redirect Url
function setFrameDocument() {
    var _hash = window.location.hash.replace("#", "");
    var _frame = document.getElementById("frame");
    var _parameters = window.location.search;
    if (_hash.length > 0) {
        loadPage(_frame, _hash, _parameters);
    }
}

///load url
function loadPage(frame, page, params) {
    console.log("/bwum" + page + params);
    window.parent.frames["frame"].location.href = "/bwum" + page + params;
}
/*页面加载控制结束*/
