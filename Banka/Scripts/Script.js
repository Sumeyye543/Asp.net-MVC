$(function () {
    //Remove the dummy row if data present.
    if ($("#tblCustomers tr").length > 2) {
        $("#tblCustomers tr:eq(1)").remove();
    } else {
        var row = $("#tblCustomers tr:last-child");
        row.find(".Edit").hide();
        row.find(".Delete").hide();
        row.find("span").html('&nbsp;');
    }
});

function AppendRow(row, customerId, name, country) {
    //Bind CustomerId.
    $(".CustomerId", row).find("span").html(customerId);

    //Bind Name.
    $(".Name", row).find("span").html(name);
    $(".Name", row).find("input").val(name);

    //Bind Country.
    $(".Country", row).find("span").html(country);
    $(".Country", row).find("input").val(country);

    row.find(".Edit").show();
    row.find(".Delete").show();
    $("#tblCustomers").append(row);
};

//Add event handler.
$("body").on("click", "#btnAdd", function () {
    var txtName = $("#txtName");
    var txtCountry = $("#txtCountry");
    $.ajax({
        type: "POST",
        url: "/Home/InsertCustomer",
        data: '{name: "' + txtName.val() + '", country: "' + txtCountry.val() + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var row = $("#tblCustomers tr:last-child");
            if ($("#tblCustomers tr:last-child span").eq(0).html() != "&nbsp;") {
                row = row.clone();
            }
            AppendRow(row, r.CustomerId, r.Name, r.Country);
            txtName.val("");
            txtCountry.val("");
        }
    });
});

//Edit event handler.
$("body").on("click", "#tblCustomers .Edit", function () {
    var row = $(this).closest("tr");
    $("td", row).each(function () {
        if ($(this).find("input").length > 0) {
            $(this).find("input").show();
            $(this).find("span").hide();
        }
    });
    row.find(".Update").show();
    row.find(".Cancel").show();
    row.find(".Delete").hide();
    $(this).hide();
});

//Update event handler.
$("body").on("click", "#tblCustomers .Update", function () {
    var row = $(this).closest("tr");
    $("td", row).each(function () {
        if ($(this).find("input").length > 0) {
            var span = $(this).find("span");
            var input = $(this).find("input");
            span.html(input.val());
            span.show();
            input.hide();
        }
    });
    row.find(".Edit").show();
    row.find(".Delete").show();
    row.find(".Cancel").hide();
    $(this).hide();

    var customer = {};
    customer.CustomerId = row.find(".CustomerId").find("span").html();
    customer.Name = row.find(".Name").find("span").html();
    customer.Country = row.find(".Country").find("span").html();
    $.ajax({
        type: "POST",
        url: "/Home/UpdateCustomer",
        data: '{customer:' + JSON.stringify(customer) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
});

//Cancel event handler.
$("body").on("click", "#tblCustomers .Cancel", function () {
    var row = $(this).closest("tr");
    $("td", row).each(function () {
        if ($(this).find("input").length > 0) {
            var span = $(this).find("span");
            var input = $(this).find("input");
            input.val(span.html());
            span.show();
            input.hide();
        }
    });
    row.find(".Edit").show();
    row.find(".Delete").show();
    row.find(".Update").hide();
    $(this).hide();
});

//Delete event handler.
$("body").on("click", "#tblCustomers .Delete", function () {
    if (confirm("Do you want to delete this row?")) {
        var row = $(this).closest("tr");
        var customerId = row.find("span").html();
        $.ajax({
            type: "POST",
            url: "/Home/DeleteCustomer",
            data: '{customerId: ' + customerId + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if ($("#tblCustomers tr").length > 2) {
                    row.remove();
                } else {
                    row.find(".Edit").hide();
                    row.find(".Delete").hide();
                    row.find("span").html('&nbsp;');
                }
            }
        });
    }
});