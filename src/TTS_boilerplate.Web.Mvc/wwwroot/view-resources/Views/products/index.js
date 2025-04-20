(function ($) {
    console.log('-- vao index Product --');
    //$(function () {
        var _$productList = $('#ProductList');

    $(document).ready(function () {
        loadProducts();
    });
    
    function createProductCard(product) {
        return `
                <div class="col-lg-2 col-md-3 col-sm-4">
                    <div class="card shadow-sm card__product" style="padding:0" data-product-id="${product.Id}">
                        <div class="card-body" style="padding:0px;">
                            <div>
                                <img src="${product.ProductImagePath}" loading="lazy" class="card-img-top card__img" alt="${product.NameProduct}" />
                                ${product.Discount ? `<span class="badge rounded-pill position-absolute top-0 end-0 bg-danger text-white">${product.Discount}%</span>` : ''}
                            </div>
                            <div class="hi">
                                <div class="row">
                                    <div class="col-12 text-start d-flex flex-column">
                                        <h5 class="card-title card__name">${product.NameProduct}</h5>
                                        <p class="text-muted mb-0">${product.NameCategory}</p>
                                        <p class="text-muted card__price">${product.Price} VND</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    function loadProducts() {
        $.ajax({
            url: abp.appPath + `Products/GetAll_Product`,
            method: 'GET',
            success: function (data) {
                console.warn('--success-- Products/GetAll_Product');
                if (!Array.isArray(data)) {
                    console.error('Dữ liệu không hợp lệ:', data);
                    return;
                }
                var html = '';
                data.forEach(product => {
                    html += createProductCard(product);
                });
                _$productList.append(html);
                page++;
            },
            error: function (e) {
                console.warn('--error', e);
                alert('Không thể tải danh sách sản phẩm. Vui lòng thử lại sau.');
            }
        });
    }

    var _$getProduct = abp.services.app.productService
    console.log('-- a',abp.services.app.productService)

})(jQuery);


