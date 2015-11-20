function recomments(value, url) {
   // var url = t > 0 ? "/ask/answerquestions" : "/column/reviews";
    var content = editor.document.getBody().getText();
    console.log(content);
    if (content.Length == 0||content==null) {
        alert("内容不能为空！");
        console.log("alert");
    }
    else {
        $.post(url, { "id": value, "commentStr": editor.document.getBody().getHtml() }, function (json) {
            var data = eval(json);
            if (data.status == "success") {
                $(".comment-list").append(data.context);
                CKEDITOR.instances.editor.setData('');
            }
            if (data.status == "warning") {
                location.href = data.url;
            }
        }, "JSON");
    }
}

$(function () {
    $(".comment-item-vote a").click(function () {
        var url = $(this).attr("data-href") + "&time=" + Date.parse(new Date());
        var aid = $(this).attr("data-aid");
        var qid = $(this).attr("data-qid");
        var op = $(this).attr("data-op");
        var _this = $(this);
        $.get(url,{"aid":aid,"qid":qid},function (json) {
            var data = eval(json);
            if (data.status == "success") {
                _this.empty();
                _this.siblings().empty();
                _this.parent().children("a").first().html("<span>" + data.pro + "</span>");
                _this.parent().children("a").first().next().html("<span>" + data.con + "</span>");
                if (op == data.op) {
                    if (data.iscancel) {
                        _this.removeClass("pressed");
                        _this.siblings().removeClass("pressed");
                    } else {
                        _this.addClass("pressed");
                        _this.siblings().removeClass("pressed");
                    }
                }
            }
            if (data.status == "warning") {
                location.href = data.url;
            }
        }, "Json")
    });
})