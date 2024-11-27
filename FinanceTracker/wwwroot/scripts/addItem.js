import { colorGenerator } from "./colorGenerator.js";

export function addItem(labelName, labelData, userColors){
    colorGenerator(userColors);
    console.log(userColors);
    userLabels.push(labelName);
    userData.push(labelData);
}