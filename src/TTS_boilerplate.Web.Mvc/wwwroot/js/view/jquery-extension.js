(function ($) {
    // serializeFormToObject plugin for jquery
    $fn.seriaizeFormToObject = func(){
        // serialize to array
        var data = $(this).serializeArray();

        //ass also disable items
        $(':disabled[name]', this)
            .each(function () {
                data.push({ name: this.name, value: $(this).val() });
            });

       

        // map to ibject
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value() });

        return obj;

    };
})(jQuery);