function sendJSON(jsonData){
    fetch('/addItem',  {
        method: "POST",
        headers: {
            'Content-Type' : 'application/json'
        },
        body : JSON.stringify(jsonData)
    })
    .then(response => {
      
        return response.json();
  })
}
function categorySender(){
    let category = document.getElementById("category");
    let count = document.getElementById("count");
    let categoryValue = category.value;
    let countValue = count.value;
    let newItem = {
        category: categoryValue,
        count: countValue,
    }
    sendJSON(newItem);s
}

