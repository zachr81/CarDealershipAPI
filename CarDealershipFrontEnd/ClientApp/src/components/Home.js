import React, { Component } from 'react';
import Checkbox from './Checkbox';

const camelSpacingRegex = /([A-Z])([A-Z])([a-z])|([a-z])([A-Z])/g;

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { selectedCheckboxes: new Set(), cars: [], loading: true, filters: ['HasLowMiles', 'IsFourWheelDrive', 'HasSunroof', 'HasPowerWindows', 'HasNavigation', 'HasHeatedSeats'], colors: ['Red', 'White', 'Gray', 'Silver', 'Black'], selectedColor: "", selectedMatchAll: "true" };
    }

    componentDidMount() {
        this.populateCarData();
    }

    optionSelected = () => {
        var e = document.getElementById("colorFilter");
        var selected = e.options[e.selectedIndex].text;

        if (selected === "Choose Any")
            this.setState({ selectedColor: "" });
        else {
            this.setState({ selectedColor: selected });
        }
    };

    searchSelected = () => {
        var e = document.getElementById("searchFilter");
        var selected = e.options[e.selectedIndex].value;
        this.setState({ selectedMatchAll: selected });
    };

    toggleCheckbox = label => {
        if (this.state.selectedCheckboxes.has(label)) {
            this.state.selectedCheckboxes.delete(label);
        } else {
            this.state.selectedCheckboxes.add(label);
        }
    }

    handleFormSubmit = formSubmitEvent => {
        formSubmitEvent.preventDefault();

        this.filterCarData(true);
    }

    createCheckbox = label => (
        <Checkbox
            label={label.replace(camelSpacingRegex, '$1$4 $2$3$5')}
            handleCheckboxChange={this.toggleCheckbox}
            key={label}
        />
    )

    createCheckboxes = () => (
        this.state.filters.map(this.createCheckbox)
    )

    getFeatures(car) {
        var featureList = "";
        if (car["color"] != null && car["color"] != "None") {
            featureList += car["color"] + ", ";
        }
        for (var feature in car) {
            if (car[feature] === true) {
                var featureWithSpaces = feature.replace(camelSpacingRegex, '$1$4 $2$3$5')
                featureList += featureWithSpaces.substring(featureWithSpaces.indexOf(" ")) + ', ';
            }
        }
        return featureList.substring(0, featureList.length - 2);
    }

    displayCars = cars => (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Year</th>
                    <th>Make</th>
                    <th>Features</th>
                </tr>
            </thead>
            <tbody>
                {cars.map(car =>
                    <tr key={car._id}>
                        <td>{car.year}</td>
                        <td>{car.make}</td>
                        <td>{this.getFeatures(car)}</td>
                    </tr>
                )}
            </tbody>
        </table>
    )

    renderCarsTable(cars) {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-sm-12">

                        <form onSubmit={this.handleFormSubmit}>
                            <div>
                                Choose a color : &nbsp;
            <select id="colorFilter" onChange={this.optionSelected}>
                                    <option value="any">Choose Any</option>
                                    {this.state.colors.map(color => {
                                        return <option value={color}>{color}</option>;
                                    })}
                                </select>
                            </div>
                            {this.createCheckboxes()}
                            <div>
                                Select search method: &nbsp;
                        <select id="searchFilter" onChange={this.searchSelected}>
                                    <option value="true">Match All Filters</option>
                                    <option value="false">Match Any Filter</option>
                                </select>
                            </div>
                            <button className="btn btn-primary" type="submit">Search</button>
                        </form>

                    </div>
                </div>
                {this.displayCars(cars)}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCarsTable(this.state.cars);

        return (
            <div>
                <h1 id="tabelLabel" >Welcome to the iTrellis Car Dealership</h1>
                {contents}
            </div>
        );
    }

    async populateCarData() {
        const response = await fetch('http://localhost:57040/cars');
        const data = await response.json();
        this.setState({ cars: data, loading: false });
    }

    async filterCarData() {
        var requestString = '[';
        for (const checkbox of this.state.selectedCheckboxes) {
            requestString += '{"fieldType":"boolean","fieldValue":"true","fieldName":"' + checkbox.replaceAll(" ", "") + '"},';
        }

        if (this.state.selectedColor != "") {
            requestString += `{"fieldType":"color","fieldValue":"${this.state.selectedColor}","fieldName":"Color"}`
        }
        else {
            requestString = requestString.substring(0, requestString.length - 1);
        }

        requestString += ']';
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: requestString
        };
        const response = await fetch(`http://localhost:57040/cars/search?matchAll=${this.state.selectedMatchAll}`, requestOptions);
        const data = await response.json();
        this.setState({ cars: data, loading: false });
    }
}
