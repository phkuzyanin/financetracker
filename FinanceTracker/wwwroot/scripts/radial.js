const ctx = document.getElementById('myChart').getContext('2d');

let userLabels = [10,100]
let userColors = ["#0ACAAA","#10Ffff"];
let userData = [10,10]
const myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: userLabels,
        datasets: [{
            label: 'Мои данные',
            data: userData,
            backgroundColor: userColors,
        }]
    },
    options: {
        plugins: {
            legend: {
                display: false,
                }
            }
        }
    
});