(function ($) {
    var _$productServices = abp.services.app.productService
    console.log('-- a', abp.services.app.productService)
    var _$productList = $('#ProductList');
    var _$modal = $('#modalCard'),
        _$valPrice = $('input[name=maxPrice');
    var _$addCartId = '',
        //_$isUser = 0,
        _$buyNowId = '';
  var _$idUser = abp.session.userId;

    var _$rangePrice = $('#price-range');
    // Khởi tạo khi document ready
    $(document).ready(function () {
        var test = $('#test').attr("data-test")
        console.warn('-- test', test);
      productDisplay.init();
      //initCart();
    });

    // Load product lên view 
  var productDisplay = {
        

        // hàm khởi tạo 
        init: function () {
            console.warn('-- da vao init--')
        this.loadProducts();

        
        },
        loadProducts : function(){
             var self = this; // Lưu context this(this này của productDisplay)
          var userId = _$idUser
             abp.ui.setBusy(); // Hiển thị loading
          // Sử dụng abp.services.app để gọi API
          _$productServices.getAll_Product()
                 .done(function (result) {
                     console.warn('-- load thanh cong', result);
                     if (result && result.items) {  // vào function này thì nó là this khác 
                         self.renderProducts(result.items);

                     } else {
                         console.error('Không có dữ liệu sản phẩm');
                         abp.notify.error('Không thể tải dữ liệu sản phẩm');
                     }
                 })
                 .fail(function (error) {
                     console.error('Lỗi khi tải sản phẩm:', error);
                     abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
                 })
                 .always(function () {
                     abp.ui.clearBusy();
                 });
          _$productServices.initCart(userId)
            .done(function () {
              console.warn('-- da tao gio hang cho user {0} thanh cong')
            })
            .fail(function () {
              console.warn('Tao gio hang that bai');
            })
        
        },
        renderProducts: function (products) {
            _$productList.empty(); // Xóa nội dung cũ trước khi render

            if (!products || products.length === 0) {
                _$productList.html('<div class="col-12 text-center">Không có sản phẩm nào</div>');
                return;
            }
            products.forEach(function (product) {
                var productHtml = `
                    <div class="col-lg-2 col-md-3 col-sm-4" >
                    <div class="card shadow-sm card__product" style="padding:0" data-product-id="${product.id}">
                        <div class="card-body" style="padding:0px;">
                            <div>
                                <img src="${product.productImagePath}" loading="lazy" class="card-img-top card__img" alt="${product.nameProduct}" />
                                ${product.discount ? `<span class="badge rounded-pill position-absolute top-0 end-0 bg-danger text-white">${product.discount}%</span>` : ''}
                            </div>
                            <div class="hi" style="background-color:white">
                                <div class="row">
                                    <div class="col-12 text-start d-flex flex-column">
                                        <h5 class="card-title card__name">${product.nameProduct}</h5>
                                        <p class="text-muted mb-0">${product.nameCategory}</p>
                                        <p class="text-muted card__price" style="margin-bottom:0px">${product.price} VND</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                `;
                _$productList.append(productHtml);
            });
        }
    };

    // hiển thị modal khi bấm vào sản phẩm 
    _$productList.on('click', '.card__product', function () {
        const productId = $(this).data('product-id');

        $('#buyNow').attr('data-product-id', productId);
        $('#addCart').attr('data-product-id', productId);

        _$buyNowId = productId;
        _$addCartId = productId;

        console.warn('-- buynow : addCart', $('#buyNow').attr('data-product-id'), $('#addCart').attr('data-product-id'));
        // Cleanup function
        function cleanup() {
            $('#modalName').text('');
            $('#modalImage').attr('src', '');
            $('#modalDescription').text('');
            $('#modalPrice').text('');
            
        }
        _$modal.on('hidden.bs.modal', cleanup);

        console.log('-- id product', productId);
        //abp.ui.setBusy($modal);
        _$productServices.getProduct(productId)
            .done(function (product) {
                console.warn('-- product item', product);
                $('#modalName').text(product.nameProduct);
                $('#modalImage').attr('src', product.productImagePath || '/images/no-image.png')
                    .on('error', function () {
                        $(this).attr('src', '/images/no-image.png');
                    });
                $('#modalDescription').text('Mô tả: ' + product.descriptionProduct);
                $('#modalPrice').text(product.price + 'VND');

                // hiển thị modal product 
                _$modal.addClass('active-modal');  // active được tạo trong card.css 
                //_$modal.modal('show');
                console.log('-- da hien modal');
            })
            .fail(function (error) {
                console.error('Lỗi khi tải thông tin sản phẩm:', error);
                abp.notify.error('Không thể tải thông tin sản phẩm. Vui lòng thử lại sau.');
            })
            .always(function () {
                // Ẩn loading
                abp.ui.clearBusy();
            });

    });

    $('#buyNow').on('click', function () {
        console.log('-- da click buyNow voi id', _$buyNowId)
    });

    $('#addCart').on('click', function () {
        _$idUser = abp.session.userId;
        console.log('Sending data:', {
            idProduct: _$addCartId,
            idUser: _$idUser
        });
        //_$isUser = abp.session.userId;
        abp.ui.setBusy();
        _$productServices.addProductToCart({
            idProduct: _$addCartId,
            idUser: _$idUser
        })
            .done(function (result) {
                console.log('-- thanh cong Add', result)
                abp.notify.info("Đã thêm sản phẩm vào giỏ hàng!")
            })
            .fail(function (error) {
                console.error('Lỗi khi tải sản phẩm:', error);
                abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
            })
            .always(function () {
                abp.ui.clearBusy();
            });
    });

    var addCardEvent = (function () {
        return {
            initAddCart: function () {
                this.bindEvent();
            },
            bindEvent: function () {
               
            },
        }
    });




    // event di chuyển thanh giá
    _$rangePrice.on('input', function () {
        const val = $(this).val();
        console.log('-- valPrice', val);
        _$valPrice.val(val);
        console.log('-- valPrice', _$valPrice.val());
    })
    // event khi nhập giá
    _$valPrice.on('input', function () {
        const val = $(this).val();
        console.log('-- valPrice', val);
    })


})(jQuery);
