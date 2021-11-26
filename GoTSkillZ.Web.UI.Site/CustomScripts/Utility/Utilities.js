var GoTSkillZCommonUtilityFunctions = {
    getCookie: function () {
        "use strict";
        var cookie = "GoTSkillZ";
        var value = "; " + document.cookie;
        var parts = value.split("; " + cookie + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    },
    getUrlParam: function (pageUrl, searchParam) {
        "use strict";

        var url = new URL(pageUrl);
        var searchParamValue = url.searchParams.get(searchParam);
        if (searchParamValue !== null && searchParamValue !== "") {
            return searchParamValue;
        } else {
            return "0";
        }
    },
    formatUrl: function (url) {
        "use strict";
        var regexp = /(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;

        if (regexp.test(url)) {
            return url;
        } else {
            return "https://" + url;
        }
    },
    validateUrl: function (url) {
        "use strict";

        var regexp = /(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;

        if (regexp.test(url)) {
            return true;
        } else {
            return false;
        }
    },
    setProfileUrlLink: function (userId) {
        "use strict";

        var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname + '?UID=' + userId;
        window.history.pushState({ path: newurl }, '', newurl);

    },
    formatBytes(bytes, decimals) {
        if (bytes == 0) return '0 Bytes';
        var k = 1024,
            dm = decimals <= 0 ? 0 : decimals || 2,
            sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
            i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
    }
}



$(window).on("load", function () {
    "use strict";

});