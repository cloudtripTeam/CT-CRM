﻿function msg(t) { function e() { o || (x.css({ opacity: 0, top: G - 50, left: N }), x.css("background-image", "url('" + msgBoxImagePath + "msgBoxBackGround.png')"), I.css({ opacity: t.opacity }), t.beforeShow(), I.css({ width: $(document).width(), height: i() }), $(d + "," + v).fadeIn(0), x.animate({ opacity: 1, top: G, left: N }, 200), setTimeout(t.afterShow, 200), $("#" + g).focus(), o = !0, $(window).bind("resize", function (t) { var e = x.width(), n = x.height(), i = $(window).height(), a = $(window).width(), o = i / 2 - n / 2, s = a / 2 - e / 2; x.css({ top: o, left: s }), I.css({ width: "100%", height: "100%" }) })) } function n() { o && (t.beforeClose(), x.animate({ opacity: 0, top: G - 50, left: N }, 200), I.fadeOut(300), setTimeout(function () { x.remove(), I.remove() }, 300), setTimeout(t.afterClose, 300), $(window).unbind("resize"), o = !1) } function i() { var t = document; return Math.max(Math.max(t.body.scrollHeight, t.documentElement.scrollHeight), Math.max(t.body.offsetHeight, t.documentElement.offsetHeight), Math.max(t.body.clientHeight, t.documentElement.clientHeight)) } function a() { x.fadeOut(200).fadeIn(200) } var o = !1, s = typeof t, l = { content: "string" == s ? t : "Message", title: "Warning", type: "alert", autoClose: !1, timeOut: 0, modal: !1, showButtons: !0, buttons: [{ value: "Ok" }], inputs: [{ type: "text", name: "userName", header: "User Name" }, { type: "password", name: "password", header: "Password" }], success: function (t) { }, beforeShow: function () { }, afterShow: function () { }, beforeClose: function () { }, afterClose: function () { }, opacity: .1 }; if (t = "string" == s ? l : t, null != t.type) switch (t.type) { case "alert": t.title = null == t.title ? "Warning" : t.title; break; case "info": t.title = null == t.title ? "Information" : t.title; break; case "error": t.title = null == t.title ? "Error" : t.title; break; case "confirm": t.title = null == t.title ? "Confirmation" : t.title, t.buttons = null == t.buttons ? [{ value: "Yes" }, { value: "No" }, { value: "Cancel" }] : t.buttons; break; case "prompt": t.title = null == t.title ? "Log In" : t.title, t.buttons = null == t.buttons ? [{ value: "Login" }, { value: "Cancel" }] : t.buttons; break; default: u = "alert.png" } t.timeOut = null == t.timeOut ? null == t.content ? 500 : 70 * t.content.length : t.timeOut, t = $.extend({}, l, t), t.autoClose && setTimeout(n, t.timeOut); var u = ""; switch (t.type) { case "alert": u = "alert.png"; break; case "info": u = "info.png"; break; case "error": u = "error.png"; break; case "confirm": u = "confirm.png"; break; default: u = "alert.png" } var c = "msgBox" + (new Date).getTime(); if (null !== navigator.userAgent.match(/msie 7/i)) var r = "msgBoxContentIEOld"; else var r = "msgBoxContent"; var d = c, m = c + "Content", h = c + "Image", p = c + "Buttons", v = c + "BackGround", g = c + "FirstButton", f = "", b = !0; $(t.buttons).each(function (t, e) { var n = ""; b && (n = ' id="' + g + '"', b = !1), f += '<input class="msgButton" type="button" name="' + e.value + '" value="' + e.value + '"' + n + "/>" }); var w = ""; $(t.inputs).each(function (t, e) { var n = e.type; w += "checkbox" == n || "radiobutton" == n ? '<div class="msgInput"><input type="' + e.type + '" name="' + e.name + '" ' + (null == e.checked ? "" : "checked ='" + e.checked + "'") + ' value="' + ("undefined" == typeof e.value ? "" : e.value) + '" /><text>' + e.header + "</text></div>" : '<div class="msgInput"><span class="msgInputHeader">' + e.header + '</span><input type="' + e.type + '" name="' + e.name + '" value="' + ("undefined" == typeof e.value ? "" : e.value) + '" ' + (void 0 !== typeof e.size ? " size='" + e.size + "' " : "") + (void 0 !== typeof e.maxlength ? " maxlength='" + e.maxlength + "' " : "") + " /></div>" }); var x, y, B, k, I, C = '<div id="' + v + '" class="msgBoxBackGround"></div>', O = '<div class="msgBoxTitle">' + t.title + "</div>", H = '<div class="msgBoxContainer"><div id="' + h + '" class="msgBoxImage"><img src="' + msgBoxImagePath + u + '"/></div><div id="' + m + '" class="' + r + '"><p><span>' + t.content + "</span></p></div></div>", T = '<div id="' + p + '" class="msgBoxButtons">' + f + "</div>", z = '<div class="msgBoxInputs">' + w + "</div>"; "prompt" == t.type ? ($("body").append(C + '<div id="' + d + '" class="msgBox">' + O + "<div>" + H + (t.showButtons ? T + "</div>" : "</div>") + "</div>"), x = $("#" + d), y = $("#" + m), B = $("#" + h), k = $("#" + p), I = $("#" + v), B.remove(), k.css({ "text-align": "center", "margin-top": "5px" }), y.css({ width: "100%", height: "100%" }), y.html(z)) : ($("body").append(C + '<div id="' + d + '" class="msgBox">' + O + "<div>" + H + (t.showButtons ? T + "</div>" : "</div>") + "</div>"), x = $("#" + d), y = $("#" + m), B = $("#" + h), k = $("#" + p), I = $("#" + v)); var E = x.width(), M = x.height(), P = $(window).height(), S = $(window).width(), G = P / 2 - M / 2, N = S / 2 - E / 2; e(), $("input.msgButton").click(function (e) { e.preventDefault(); var i = $(this).val(); if ("prompt" != t.type) t.success(i); else { var a = []; $("div.msgInput input").each(function (t, e) { var n = $(this).attr("name"), i = $(this).val(), o = $(this).attr("type"); "checkbox" == o || "radiobutton" == o ? a.push({ name: n, value: i, checked: $(this).attr("checked") }) : a.push({ name: n, value: i }) }), t.success(i, a) } n() }), I.click(function (e) { t.modal || (!t.showButtons || t.showButtons && t.buttons.length < 2 || t.autoClose ? n() : a()) }) } var msgBoxImagePath = "/controls/msgbox/images/"; jQuery.msgBox = msg;