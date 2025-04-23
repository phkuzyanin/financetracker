const ctx = document.getElementById('doughnutChart').getContext('2d');
        
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ['Красный', 'Синий', 'Желтый', 'Зеленый'],
            datasets: [{
                data: [30, 20, 15, 35],
                backgroundColor: [
                    '#FF6384',
                    '#36A2EB',
                    '#FFCE56',
                    '#4BC0C0'
                ],
                borderWidth: 0 
            }]
        },
        options: {
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    enabled: true
                }
            },
            cutout: '70%'
        }
    });
