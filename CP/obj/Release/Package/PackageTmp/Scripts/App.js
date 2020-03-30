



function LoadDropDown() {
    $('.demo-cs-multiselect').chosen({ width: '100%' });
    $("#message").fadeOut(5000);
}



function LoadPhone(PhoneValue, MobileExtension) {
    if (PhoneValue == '') {
        $(".cellno").intlTelInput();
        $(".cellno").intlTelInput("setNumber", "+91");
      
    }
    else {
      
        $(".cellno").intlTelInput();
        $(".cellno").val(PhoneValue);
      
    }
}

function Add(Id, Name, Action, Controller, AddModel, loading) {
     AddModel = $("#AddModel_");
     loading = $("#loading");
    loading.removeClass("loadingImg");
    AddModel.parent().addClass("Popuploading");
        var Bname = Name;
        if (name != "") {
            Bname = name;
        }

        if (Id > 0) {
            $.ajax({
                url: "./"+Controller+"/"+Action+"",
                type: 'GET',
                data: { id: Id },
                success: function (result) {
                    $("#AddModel_").html(result);
                    Bname = Action + ' ' + Name;
                    $('#name').html(Bname);
                    loading.addClass("loadingImg");
                    AddModel.parent().removeClass("Popuploading");
                    $(".modal").css("display", "block");
                }
            });
        }
        else {
            $.ajax({
                url:  "./" + Controller + "/" + Action + "",
                type: 'GET',
                success: function (result) {
                    $("#AddModel_").html(result);
                    Bname = Action + ' ' + Name;
                    $('#name').html(Bname);
                    $("#loading").addClass("loadingImg");
                    AddModel.parent().removeClass("Popuploading");
                    $(".modal").css("display", "block");
                }
            });
    }
}
function ShowReportItems(ReportGroup,Code,ParentReportCode,Entity,Category, Action, Controller, AddModel, loading)
{
    
    AddModel = $("#AddModel_");
    loading = $("#loading");
    loading.removeClass("loadingImg");
    AddModel.parent().addClass("Popuploading");
    var Bname = Code;
    if (name != "") {
        Bname = Code;
    }
            $.ajax({
            url: "./" + Controller + "/" + Action + "",
            type: 'GET',
                data: { ReportGroup: ReportGroup, ReportCode:Code, Category: Category, ReportItemType: Entity, ParentReportCode: ParentReportCode, },
            success: function (result) {
                $("#AddModel_").html(result);
                Bname = 'Edit' + ' ' + Code;
                $('#name').html(Bname);
                $("#loading").addClass("loadingImg");
                AddModel.parent().removeClass("Popuploading");
                $(".modal").css("display", "block");
              
            }
        });
 
}



function SubmitForm(frm, res) {
  
    $('#loading-img-save').show();
    document.getElementById('save').disabled = "true";
    $.ajax({
        url: frm.action,
        type: 'POST',
        data: new FormData(frm),
        processData: false,
        contentType: false,
        success: function (responseText, textStatus, XMLHttpRequest) {
            $('#AddModel_').html(responseText);
            var generalErrors = $(".validation-summary-errors li");
            try {
                $(".input-validation-error").tooltip({
                    trigger: 'focus',
                    placement: 'top'
                });
                $(".input-validation-error")[0].focus();
            } catch (e) {
            }
            var message = res;
            if (message.length > 0) {
                document.getElementById('name').focus();
            }
        }
    });
    return false;
}

function GetCursorLocation(CurrentTextBox) {
    var CurrentSelection, FullRange, LocationIndex = -1;
    if (typeof CurrentTextBox.selectionStart == "number") {
        LocationIndex = CurrentTextBox.selectionStart;
    }
    else if (document.selection && CurrentTextBox.createTextRange) {
        CurrentSelection = document.selection;
        if (CurrentSelection) {
            FullRange = CurrentTextBox.createTextRange();
            LocationIndex = FullRange.text.length;
        }
    }
    return LocationIndex;
}

function ShowBootstrapTable(Controller,Action,Status,TableId)
{
   
    $.ajax({
        method: 'Get',
        url: "./" + Controller + "/" + Action + "",
        data: { Status: Status },
        dataType: 'json',
        success: function (DataList) {  
            $('#'+ TableId).bootstrapTable({
                exportOptions: {
                    fileName: TableId
                },
                exportTypes: ['excel'],
                cache: false,
                striped: true,
                pagination: true,
                pageSize: 10,
                pageList: [10, 25, 50, 100, 200],
                search: true,
                showRefresh: true,
                minimumCountColumns: 2,
                data: DataList
            });
        },
        error: function (e) {
            console.log(e.responseText);
        }
    });
    LoadDropDown();
    
}

function ShowDeletedRecords(Controller, Action, Status, TableId) {
    var label = $("#DeleteItemsBtn").text();
    if (label == "Deleted Items") {
        $("#DeleteItemsBtn").text("General Items");
        $("#AddBtn").hide();
        $(TableId).bootstrapTable('refresh', {
            url: "./" + Controller + "/" + Action + "?Status=" + 0

        });
    }
    else {
        $("#DeleteItemsBtn").text("Deleted Items");
        $("#AddBtn").show();
        $(TableId).bootstrapTable('refresh', {
            url: "./" + Controller + "/" + Action + "?Status=" + 1

        });

    }
}

function transferSecondary(s, t, p, IsRemove) {
    var opt;
    try {
        opt = s.options[s.selectedIndex];
        t.options[t.options.length] = new Option(opt.innerHTML, opt.value);
        if (IsRemove == true) {
            p.options[t.options.length] = new Option(opt.innerHTML, opt.value);
        }
        else {
            for (var i = 0; i < p.length; i++) {
                if (p.options[i].value == opt.value) {
                    p.remove(i);
                }
            }
        }
        s.options.remove(s.selectedIndex);
    } catch (e) { alert('Please select an analyst to move'); }
}










  



    

