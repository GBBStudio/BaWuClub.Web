$(function () {
    $("#avatar-upload").uploadify({
        height: 30,
        width: 120,
        buttonText:'选择头像',
        swf: '/content/uploadify/uploadify.swf',
        uploader: '/fileupload/uploadavatar',
        overrideEvents: ['onSelectError', 'onDialogClose'],
        onSelectError: function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("您好目前只能上传1张图片！");
                    break;
                case -110:
                    alert("您好上传文件不能大于1M！");
                    break;
                case -120:
                    alert("您好文件大小异常！");
                    break;
                case -130:
                    alert("您好文件类型异常！");
                    break;
            }
            return false;
        },
        onUploadSuccess: function (file, data, response) {
            var obj = jQuery.parseJSON(data);
            $(".avatar-upload").hide();
            $(".avatar-wrap").html("<img src=\"/uploads/avatar/" + obj.name + "\" alt=\"\"/><input value=\"" + obj.name + "\" type=\"hidden\" name=\"cover\"/><a href=\"#\" class=\"avatar-del\">删除</a>");
        }
    });

    $(document).on("click", ".avatar-del", function () {
        $(".avatar-upload").show();
        $(".avatar-wrap").empty();            
    })
});