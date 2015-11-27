function reviews(value,url) {
    var content = editor.document.getBody().getText();
    if (content.length == 0) {
        alert("内容不能为空！");
    }
    else {
        $.post(url, { "id": value, "context": editor.document.getBody().getHtml() }, function (json) {
            var data = eval(json);
            if (data.status == "success") {
                $(".topic-s-reviews-list ul").append(data.context);
                CKEDITOR.instances.editor.setData('');
            }
            if (data.status == "warning") {
                location.href = data.url;
            }
        }, "JSON");
    }
}

function takeid(id,tid,s) {
    var params = { "id": id, "tid":tid, "s": s };
    $.post("/forum/take", params, function (data) {
        if(data.status=="warning"){
            window.location.href=data.url;
        } else if (data.status == "error") {
            alert(data.context);
        } else if (data.status == "success") {
            var user = JSON.parse(data.context);
            alert("操作成功！感谢您的参与");
            var cover = "/content/images/no-img.jpg";
            if (user.cover != null && user.cover.length > 0)
                cover = "/uploads/avatar/small/" + user.cover;
            $(".topic-join-tips").html("<div class=\"topic-isJoined\"><span>您已经参加了该任务！</span></div>");
            $(".join-user").append("<a href=\"/member/u-" + user.id + "/show\"><img src=\"" + cover + "\"/><span>@" + user.name + "</span></a>");
        }
    });
}