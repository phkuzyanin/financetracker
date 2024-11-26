const ctx = document.getElementById('myChart').getContext('2d');
let userLabels = ['Жопа','АХАХАХАХАХАХА','Яндекс','Шрек']
let userColors = [
    'rgb(255, 99, 132)',
    'rgb(54, 162, 235)',
    'rgb(255, 206, 86)',
    'rgb(75, 192, 192)'
];
let userData = [12, 19, 3, 5]
function colorGenerator(){
    return(`rgb(${Math.floor(Math.random()*256)},${Math.floor(Math.random()*256)},${Math.floor(Math.random()*256)})`)
}
function addItem(labelName, labelData){
    userColors.push(colorGenerator());
    console.log(userColors);
    userLabels.push(labelName);
    userData.push(labelData);
}
addItem('Жижа', 100);
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