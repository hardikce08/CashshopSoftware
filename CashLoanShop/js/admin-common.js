
$(function () {

    geo_search_init();


    $("body").on("click", ".selectall", function () {
        var ischecked = this.checked;
        console.log('new state: ' + ischecked);

        $(this).parents().filter("table").find("input[type=checkbox]").each(function () {
            this.checked = ischecked;

        });
    });

    $('.datepicker').each(function () {
        var $this = $(this);
        var def = $this.val();

        console.log(def);

        $this.datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            setDate: def,
            dateFormat: 'dd-M-yy'
        });
    });


});


function geo_search_init() {

    console.log("suburbs_search()");

    $('.suburbsearch').each(function () {
        var $this = $(this);
        var country = $("#" + $this.data("country"));
        var state = $this.data("town");
        var postcode = $this.data("postcode");
        var base = siteurl;
        
        $this.typeahead({
            items: 50,
            source: function (query, process) {
                $.get(base + 'ajax/suburbs', { q: query, limit: 20, cn: $(country).val() }, function (data) {
                    labels = [];
                    mapped = {};
                    $.each(data, function (i, datum) {
                        var query_label = datum.Name + ', ' + datum.City + ', ' + datum.PostCode;
                        mapped[query_label] = datum;
                        labels.push(query_label);
                    });
                    process(labels);
                }, 'json')
            },
            // Method fired when a result is selected.
            updater: function (query_label) {
                var datum = mapped[query_label];
                $('#' + state).val(datum.City);
                $('#' + postcode).val(datum.PostCode);

                console.log('State : ' + state);


                return datum.Name;
                // Here gonna add some js to save contact id to hidden input 
                // ...

                // If user selects an item, the inputed value will be for example "AC/DC - Shoot To Thrill {3}"
                var input_label = query_label + '{' + contact.id + '}';
                return input_label;

            },
            // Method responsible for element view
            highlighter: function (query_label) {
                var contact = mapped[query_label];
                var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');

                var highlighted_label = query_label.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                    return '<strong>' + match + '</strong>'
                });

                // Item will be viewed as "AC/DC - Shoot To Thrill (Back to Black)
                var view_label = highlighted_label; // +' (<i>' + contact.consignee + '</i>)';
                return view_label;
            }
        });
    });

    $('.statesearch').each(function () {
        var $this = $(this);
        var country = $("#" + $this.data("country"));
        var postcode = $this.data("postcode");
        var base = siteurl;

        $this.typeahead({
            items: 50,
            source: function (query, process) {
                $.get(base + 'ajax/states', { q: query, limit: 20, cn: $(country).val() }, function (data) {
                    labels = [];
                    mapped = {};
                    $.each(data, function (i, datum) {
                        var query_label = datum.City + ', ' + datum.PostCode;
                        mapped[query_label] = datum;
                        labels.push(query_label);
                    });
                    process(labels);
                }, 'json')
            },
            // Method fired when a result is selected.
            updater: function (query_label) {
                var datum = mapped[query_label];
                //$('#' + state).val(datum.City);
                $('#' + postcode).val(datum.PostCode);

                //console.log('State : ' + state);


                return datum.City;
                // Here gonna add some js to save contact id to hidden input 
                // ...

                // If user selects an item, the inputed value will be for example "AC/DC - Shoot To Thrill {3}"
                var input_label = query_label + '{' + contact.id + '}';
                return input_label;

            },
            // Method responsible for element view
            highlighter: function (query_label) {
                var contact = mapped[query_label];
                var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');

                var highlighted_label = query_label.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                    return '<strong>' + match + '</strong>'
                });

                // Item will be viewed as "AC/DC - Shoot To Thrill (Back to Black)
                var view_label = highlighted_label; // +' (<i>' + contact.consignee + '</i>)';
                return view_label;
            }
        });
    });




    $('.streetsearch').each(function () {
        var $this = $(this);
        var country = $("#" + $this.data("country"));
        var suburb = $this.data("suburb");
        var state = $this.data("town");
        var postcode = $this.data("postcode");

        var group = $this.data('group');

        //var country = $('#' + $this.data('group')).val();

        $this.typeahead({
            items: 50,
            source: function (query, process) {
                $.get(siteurl + 'ajax/streets', { q: query, limit: 20, cn: $(country).val() }, function (data) {
                    labels = [];
                    mapped = {};
                    $.each(data, function (i, datum) {
                        var query_label = datum.Street + ', ' + datum.Suburb + ', ' + datum.City + ' ' + datum.PostCode;
                        mapped[query_label] = datum;
                        labels.push(query_label);

                        console.log(query + ' -> ' + datum.Street)
                    });
                    process(labels);
                }, 'json')
            },
            // Method fired when a result is selected.
            updater: function (query_label) {
                var datum = mapped[query_label];
                //$('#' + state).val(datum.City);
                $('#' + suburb).val(datum.Suburb);
                $('#' + state).val(datum.City);
                $('#' + postcode).val(datum.PostCode);

                console.log('Street : ' + datum.Street);


                return datum.Street;
                // Here gonna add some js to save contact id to hidden input 
                // ...

                // If user selects an item, the inputed value will be for example "AC/DC - Shoot To Thrill {3}"
                var input_label = query_label + '{' + contact.id + '}';
                return input_label;

            },
            // Method responsible for element view
            highlighter: function (query_label) {
                var contact = mapped[query_label];
                var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');

                var highlighted_label = query_label.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                    return '<strong>' + match + '</strong>'
                });

                // Item will be viewed as "AC/DC - Shoot To Thrill (Back to Black)
                var view_label = highlighted_label; // +' (<i>' + contact.consignee + '</i>)';
                return view_label;
            }
        });
        //$('.tt-hint').addClass('form-control');
    });





    $('#nav-search-input').typeahead({
        items: 20,
        source: function (query, process) {
            $.get(siteurl + 'ajax/sites', { q: query, limit: 20 }, function (data) {
                labels = [];
                mapped = {};
                $.each(data, function (i, datum) {
                    var query_label = datum.SiteName;
                    mapped[query_label] = datum;
                    labels.push(query_label);

                    console.log(query + ' -> ' + datum.SiteName)
                });
                process(labels);
            }, 'json')
        },
        // Method fired when a result is selected.
        updater: function (query_label) {
            var datum = mapped[query_label];            
            console.log(datum.Url);
            window.location = datum.Url;            
            return datum.AccountName;
        },
        // Method responsible for element view
        highlighter: function (query_label) {
            var contact = mapped[query_label];
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');

            var highlighted_label = query_label.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                return '<strong>' + match + '</strong>'
            });

            // Item will be viewed as "AC/DC - Shoot To Thrill (Back to Black)
            var view_label = highlighted_label; // +' (<i>' + contact.consignee + '</i>)';
            return view_label;
        }
    });



}
