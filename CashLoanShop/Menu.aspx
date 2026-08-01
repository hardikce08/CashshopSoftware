<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CashLoanShop.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #Notification1 {
            border-radius: 25px;
            background: #fe9365; /* linear-gradient(to right,#fe9365,#feb798);*/
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification2 {
            border-radius: 25px;
            background: linear-gradient(to right,#0ac282,#0df3a3);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification3 {
            border-radius: 25px;
            background: linear-gradient(to right,#fe5d70,#fe909d);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification4 {
            border-radius: 25px;
            background: linear-gradient(to right,#01a9ac,#01dbdf);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification5 {
            border-radius: 25px;
            background: linear-gradient(to right,#d4166c,#f8bad6);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification6 {
            border-radius: 25px;
            background: linear-gradient(to right,#b014ff,#f3dbff);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification7 {
            border-radius: 25px;
            background: linear-gradient(to right,#e40505,#fdb3b3);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification8 {
            border-radius: 25px;
            background: linear-gradient(to right,#415dfe,#8d9efe);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }

        #Notification9 {
            border-radius: 25px;
            background: linear-gradient(to right,#059cf9,#8bd2fd);
            color: #ffffff;
            padding: 20px;
            width: 200px;
            height: 150px;
        }
        /*https://colorlib.com//polygon/adminty/default/color.html */
        .commonbox {
            margin: 6px;
            width: 270px !important;
            height:75px !important;
                margin-left: 45px;
        }

        h6 {
            font-family: Verdana;
            font-size: large;
        }

        h3 {
            font-family: verdana;
        }

        .infobox {
            width: auto;
            height: auto;
            background: linear-gradient(to right,#fe9365,#feb798);
            padding: 20px;
            border-radius: 25px;
        }
        .panel-body {
    padding: 0px;
}
        .panel {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <table id="data" runat="server">
        <tr>
            <td style="font-family: Arial; font-weight: bold; color: black;">Important Message
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-weight: bold; color: black;">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="page-content">
        <div class="col-xs-12">
            <div class="row">
                <div class="col-md-10">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Last 24hr Stats</h3>
                    </div>
                    <div class="panel-body">
                        <div id="Notification1" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <b><asp:Label ID="lblCustomerAdded" runat="server"></asp:Label></b> Customers Added</h4>
                            </div>
                        </div>
                        <div id="Notification2" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <asp:Label ID="lblPaydayLoanOpen" runat="server"></asp:Label> Payday Loans Open</h4>
                                 
                            </div>
                        </div>

                        <div id="Notification3" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <asp:Label ID="lblPaydayClosed" runat="server"></asp:Label> Payday Loans Closed</h4>
                                
                            </div>
                        </div>
                        <div id="Notification7" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <asp:Label ID="lblTermOpen" runat="server"></asp:Label> Term Loans Open</h4>
                                
                            </div>
                        </div>
                        <div id="Notification4" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <asp:Label ID="lblTermClosed" runat="server"></asp:Label> Term Loans Closed</h4>
                                
                            </div>
                        </div>
                        <div id="Notification5" class="col-md-3 commonbox">
                            <div class="card-block">
                                <h4>
                                    <asp:Label ID="lblAmountofLoan" runat="server"></asp:Label> $ Amount Given</h4>
                                 
                            </div>
                        </div>

                    </div>
                </div>
                <%--  <div class="infobox">
                        <div class="infobox-data">
                            <span class="infobox-data-number">1245</span>
                            <div class="infobox-content">No of Loan Opened</div>
                        </div>

                    </div>
                     <div class="infobox">
                        <div class="infobox-data">
                            <span class="infobox-data-number">1245</span>
                            <div class="infobox-content">No of Loan Opened</div>
                        </div>

                    </div>--%>
                    </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="widget-box">
                            <div class="widget-header widget-header-flat widget-header-small">
                                <h5>
                                    <i class="icon-signal"></i>
                                    Payday Loan Amount Given Over Last 7 days
                                </h5>
                            </div>
                            <div>
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <canvas id="myChart" width="400" height="250"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnUserId" runat="server" />
                        <asp:HiddenField ID="hdnStoreId" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <div class="widget-box">
                            <div class="widget-header widget-header-flat widget-header-small">
                                <h5>
                                    <i class="icon-signal"></i>
                                    No of Payday Loan Given Over Last 7 days
                                </h5>
                            </div>
                            <div>
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <canvas id="NoofLoanChart" width="400" height="250"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="js/Chart.min.js"></script>
    <script>
        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "Menu.aspx/GetChartData",
                data: JSON.stringify({ storeid: $('#content_hdnStoreId').val(), userid: $('#content_hdnUserId').val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //Loan Line chart
                    var ctx = document.getElementById('myChart');
                    var myChart = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: response.d[0],
                            datasets: [{
                                label: '$ Amount Given',
                                data: response.d[1],
                                backgroundColor: [
                                    'rgba(255, 99, 132, 0.2)',
                                    'rgba(54, 162, 235, 0.2)',
                                    'rgba(255, 206, 86, 0.2)',
                                    'rgba(75, 192, 192, 0.2)',
                                    'rgba(153, 102, 255, 0.2)',
                                    'rgba(255, 159, 64, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(54, 162, 235, 1)',
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(153, 102, 255, 1)',
                                    'rgba(255, 159, 64, 1)'
                                ],
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                            }
                        }
                    });


                    ctx = document.getElementById('NoofLoanChart');
                    myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: response.d[0],
                            datasets: [{
                                label: 'No. of Loan given',
                                data: response.d[2],
                                backgroundColor: [
                                    'rgba(255, 99, 132, 0.2)',
                                    'rgba(54, 162, 235, 0.2)',
                                    'rgba(255, 206, 86, 0.2)',
                                    'rgba(75, 192, 192, 0.2)',
                                    'rgba(153, 102, 255, 0.2)',
                                    'rgba(255, 159, 64, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(54, 162, 235, 1)',
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(153, 102, 255, 1)',
                                    'rgba(255, 159, 64, 1)'
                                ],
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                            }
                        }
                    });

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
    </script>

</asp:Content>
