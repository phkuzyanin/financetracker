const ctx = document.getElementById('myChart').getContext('2d');

let userLabels = []
let userColors = [];
let userData = []
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