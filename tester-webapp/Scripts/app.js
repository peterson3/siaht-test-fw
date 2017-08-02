$(document).ready(function () {
    $('#testCaseTable').DataTable({
        "pagingType": "full_numbers"
    });



});


function populateChart(apr, rep, pend) {

    Morris.Donut({
        element: 'donut-chart',
        data: [{
            label: "Aprovados",
            value: apr
        }, {
            label: "Reprovados",
            value: rep
        }, {
            label: "Pendentes",
            value: pend
        }],
        colors: [

          '#5cb85c',
          '#d9534f',
           '#337ab7'
        ],
        backgroundColor: '#FFFFF',
        labelColor: '#000   ',
        resize: true
    });
}