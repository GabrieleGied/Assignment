import React from 'react';
import ReactDOM from 'react-dom';

class Array extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            input: "",
            result: ""
        }
    }

    handleChange = (event) => {
            this.setState({
                input: event.target.value
            })
        }

    handleSubmit = (event) => {
            if (event.keyCode === 13) {         
                var inputArray = null;
                var inputSplits = this.state.input.split(/\s*,\s*/);

                try {
                    inputArray = inputSplits.map(function (x) {
                        var inputInt = parseInt(x, 10);
                        if (!isNaN(inputInt)) {
                            return inputInt;
                        }
                        else {
                            throw "Invalid input. Please try again!";
                        }
                    });
                }
                catch (e) {
                    alert("Something went wrong. " + e);
                }

                if (inputArray) {
                    var postData = { array: inputArray };
                    var thisCallback = this;

                    $.ajax({
                        type: "POST",
                        url: "/Jump/GetJumpObject",
                        data: postData,
                        success: function (data) {
                            thisCallback.setState({       
                                result: data.Result
                            })
                        },
                        dataType: "json",
                        traditional: true
                    });
                }       
            }

            return null;
    }

    render() {
        return (
            <div className="input-container-wrapper">
                <h1>Insert an array below</h1>
                <div className="input-container">
                    <input
                        value={this.state.input}
                        onChange={this.handleChange}
                        placeholder="e.x. 1, 2, 3, 4, 5"
                        onKeyDown={this.handleSubmit}
                    />
                </div>
                <div className="input-container-result">{this.state.result}</div>
            </div>
        );
    }
}

ReactDOM.render(<Array />, document.getElementById('root'));