$('#addtocart').submit(function (event) {
    $.ajax({ // make an AJAX request
        type: "GET",
        url: "/api/DataRetrieval/AddToCart", // it's the URL of your component B
        data: $("#addtocart").serialize(), // serializes the form's elements
        success: function (data) {
            // show the data you got from B in result div
            $("#errordiv").html(data);
        }
    });
    event.preventDefault(); // avoid to execute the actual submit of the form
});