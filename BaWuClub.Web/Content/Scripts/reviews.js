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
        }
        //alert(data);
    });
}