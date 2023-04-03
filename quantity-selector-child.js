; (function ($, window, document, undefined) {

    "use strict child";
    var pluginName = "quantitySelectorChild",
        defaults = {
            propertyName: "value"
        },
        _qs1,
        _$html1;


    function Plugin(element, options) {
        // _this = this;
        _qs1 = this;
        _qs1.element = element;
        _qs1.$el = $(element);
        _qs1.settings = $.extend({}, defaults, options);
        _qs1._defaults = defaults;
        _qs1._name = pluginName;

        _qs1.init();
    }

    $.extend(Plugin.prototype, {
        init: function () {
            _$html1 = $('html');
            _qs1.boxClassName = 'quantity-selector-box';
            _qs1.$box = _qs1.$el.find('.' + _qs1.boxClassName);
            _qs1.$input = _qs1.$el.find('input');
            _qs1.title = _qs1.$el.data('increment');
            _qs1.$increment = _qs1.$el.find('.quantity-selector-incrementChd a');
            _qs1.$decrement = _qs1.$el.find('.quantity-selector-decrementChd a');
            _qs1.$current = _qs1.$el.find('.quantity-selector-currentChd');
            _qs1.currentInt = parseInt(_qs1.$current.text(), 10),
            _qs1.sTitle = _qs1.title.substring(0, _qs1.title.length - 1);
            _qs1.setTitle;

            _qs1.inputFocus1();
            _qs1.increment1();
            _qs1.decrement1();
            _qs1.boxHide1();
        },
        inputFocus1: function () {
            _qs1.fireEvent(_qs1.$input, 'focus', function (e) {
                _qs1.$box.addClass('show');
                // console.log(_qs.$input.val());
                if (!_qs1.$input.val()) {
                    _qs1.setTitle = _qs1.sTitle;
                    _qs1.inputValue1(0);
                }
            });
        },
       
        inputValue1: function (v) {
            if (v === 0)
                _qs1.$input.val(_qs1.currentInt + ' ' + _qs1.setTitle);
            else {
                var totPax = $("#totPax").val();
                totPax = parseInt(totPax);
                totPax = parseInt(totPax) + v;
                _qs1.$input.val(totPax + ' ' + _qs1.setTitle);

            }
        },

        writeCurrent: function () {
            _qs1.$current.html(_qs1.currentInt);
        },
        increment1: function () {
            _qs1.fireEvent(_qs1.$increment, 'click', function (e) {
                e.preventDefault();
                _qs1.setTitle = _qs1.title;
                _qs1.currentInt++;
                _qs1.writeCurrent();
                _qs1.inputValue1(1);
            });
        },
        decrement1: function () {
            _qs1.fireEvent(_qs1.$decrement, 'click', function (e) {
                e.preventDefault();
                if (_qs1.currentInt > 0) {
                    _qs1.setTitle = _qs1.title;
                    if (_qs1.currentInt === 1) {
                        _qs1.setTitle = _qs1.sTitle;
                    }
                    _qs1.currentInt--;
                    _qs1.writeCurrent();
                    _qs1.inputValue1(-1);
                }
            });
        },
        boxHide1: function () {
            _qs1.fireEvent($('html'), 'click', function (e) {
                if (e.target !== _qs1.$input[0] && e.target !== _qs1.$box[0] && !$(e.target).parents('.' + _qs1.boxClassName).length) {
                    _qs1.$box.removeClass('show');
                }
            });
        },
        fireEvent: function (el, evt, callback) {
            el.on(evt, { _qs1: _qs1 }, function (e) {
                _qs1 = e.data._qs1;
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