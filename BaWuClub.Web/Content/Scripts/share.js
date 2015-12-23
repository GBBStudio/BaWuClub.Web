$(function () {
    $(".sns-share a").each(function () {
        var _this = $(this);
        var t=_this.attr("data-cmd")
        if (t.length > 0) {
            $(this).attr("href",geturl(t)+window.location.href+"&title="+document.title)
        }
    })
})

function geturl(t) {
    var url = "";
    switch (t) {
        case "sina":
            url = "http://service.weibo.com/share/share.php?url=";
            break;
        case "qzone":
            url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=";
            break;
        case "renren":
            url = "http://widget.renren.com/dialog/share?resourceUrl=";
            break;
        case "douban":
            url = "http://www.douban.com/share/service?href=";
            break;
        default:
            brea;
    }
    return url;
}