(function ($) {
    $(function () {
        var _$formCreate = $('#ProductCreateForm');

        _$formCreate.find('input:first').focus();

        _$formCreate.validate();

        _$formCreate.find('button[type=submit]')
            .click(function (e) {

                e.preventDefault();

                if (!_$formCreate.valid()) {
                    return;
                }

                var inputForm = _$formCreate.serializeFormToObject();

                //abp.services.app.product.create(inputForm)
                //    .done(function(){
                //        location.href = '/Products';
                //    })

                if (abp.services.app.productService)
                {
                    //console.log(abp.app.productService.create)
                    abp.services.app.productService.create(inputForm)
                        
                        .done(function() {
                            location.href = '/Products';
                        });
                    alert("hi thanh cong")
                } else {
                    console.error('abp.services.app.product is not defined');

                    console.error(abp.services.app)

                    console.error(abp.services.app.productService)
                }
                
                //location.href = '/Products';
            });
    });

    //$(function () {
    //    $('.btn_1').click(function () {
    //        location.href = '/Products';
    //    });
    //});
})(jQuery);