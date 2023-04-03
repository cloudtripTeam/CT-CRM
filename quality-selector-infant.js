; (function ($, window, document, undefined) {

    "use strict infant";
    var pluginName = "quantitySelectorInfant",
        defaults = {
            propertyName: "value"
        },
        _qs2,
        _$html2;


    function Plugin(element, options) {
        // _this = this;
        _qs2 = this;
        _qs2.element = element;
        _qs2.$el = $(element);
        _qs2.settings = $.extend({}, defaults, options);
        _qs2._defaults = defaults;
        _qs2._name = pluginName;

        _qs2.init();
    }

    $.extend(Plugin.prototype, {
        init: function () {
            _$html2 = $('html');
            _qs2.boxClassName = 'quantity-selector-box';
            _qs2.$box = _qs2.$el.find('.' + _qs2.boxClassName);
            _qs2.$input = _qs2.$el.find('input');
            _qs2.title = _qs2.$el.data('increment');
            _qs2.$increment = _qs2.$el.find('.quantity-selector-incrementInf a');
            _qs2.$decrement = _qs2.$el.find('.quantity-selector-decrementInf a');
            _qs2.$current = _qs2.$el.find('.quantity-selector-currentInf');
            _qs2.currentInt = parseInt(_qs2.$current.text(), 10),
            _qs2.sTitle = _qs2.title.substring(0, _qs2.title.length - 1);
            _qs2.setTitle;

            _qs2.inputFocus2();
            _qs2.increment2();
            _qs2.decrement2();
            _qs2.boxHide2();
        },
        inputFocus2: function () {
            _qs2.fireEvent(_qs2.$input, 'focus', function (e) {
                _qs2.$box.addClass('show');
                // console.log(_qs.$input.val());
                if (!_qs2.$input.val()) {
                    _qs2.setTitle = _qs2.sTitle;
                    _qs2.inputValue2(0);
                }
            });
        },
        inputValue2: function (v) {
            if (v === 0)
                _qs2.$input.val(_qs2.currentInt + ' ' + _qs2.setTitle);
            else {
                var totPax = $("#totPax").val();
                totPax = parseInt(totPax);
                totPax = parseInt(totPax) + v;
                _qs2.$input.val(totPax + ' ' + _qs2.setTitle);

            }
        },
        writeCurrent: function () {
            _qs2.$current.html(_qs2.currentInt);
        },
        increment2: function () {
            _qs2.fireEvent(_qs2.$increment, 'click', function (e) {
                e.preventDefault();
                _qs2.setTitle = _qs2.title;
                _qs2.currentInt++;
                _qs2.writeCurrent();
                _qs2.inputValue2(1);
            });
        },
        decrement2: function () {
            _qs2.fireEvent(_qs2.$decrement, 'click', function (e) {
                e.preventDefault();
                if (_qs2.currentInt > 0) {
                    _qs2.setTitle = _qs2.title;
                    if (_qs2.currentInt === 1) {
                        _qs2.setTitle = _qs2.sTitle;
                    }
                    _qs2.currentInt--;
                    _qs2.writeCurrent();
                    _qs2.inputValue2(-1);
                }
            });
        },
        boxHide2: function () {
            _qs2.fireEvent($('html'), 'click', function (e) {
                if (e.target !== _qs2.$input[0] && e.target !== _qs2.$box[0] && !$(e.target).parents('.' + _qs2.boxClassName).length) {
                    _qs2.$box.removeClass('show');
                }
            });
        },
        fireEvent: function (el, evt, callback) {
            el.on(evt, { _qs2: _qs2 }, function (e) {
                _qs2 = e.data._qs2;
                callback(e);
            });
        }
    });





    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" +
                    pluginName, new Plugin(this, options));
            }
        });
    };

})(jQuery, window, document);