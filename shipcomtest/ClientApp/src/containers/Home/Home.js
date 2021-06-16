import React, { useState, useEffect } from 'react'
import { Select, MenuItem } from '@material-ui/core/'
import './Home.scss'
import resistance from '../../assets/resistor-color-diagram-bg.webp'
import { getResistanceOHMSValueService } from '../../services/resistance.service'


const Home = () => {

    const [firstBand, setFirstBand] = useState(false)
    const [secondBand, setSecondBand] = useState(false)
    const [multiplierBand, setMultiplierBand] = useState(false)
    const [toleranceBrand, setToleranceBand] = useState(false)
    const [ohmsValue, setOhmsValue] = useState(false)

    const handleFirstBand = (event) => {
        setFirstBand(event.target.value)
    }

    const handleSecondBand = (event) => {
        setSecondBand(event.target.value)
    }

    const handleMultiplierBand = (event) => {
        setMultiplierBand(event.target.value)
    }

    const handleToleranceBand = (event) => {
        setToleranceBand(event.target.value)
    }

    const callResistanceService = () =>{
        getResistanceOHMSValueService(firstBand, secondBand, multiplierBand, toleranceBrand)
        .then(res => {
            setOhmsValue(res)
        })
    }

    useEffect(() => {
        if (firstBand && secondBand && multiplierBand && toleranceBrand) {
            callResistanceService()
        }
    }, [firstBand, secondBand, multiplierBand, toleranceBrand])

    return (
        <div className='home'>
            <h2 className='title'>4 Strip Resistor Calculator</h2>
            <div className='body'>
                <div className='resistor-params'>
                        <div className='first-band-selector'>
                            <h4>1st Band of Color</h4>
                            <Select className='select' labelId="first-band-selector" value={firstBand} onChange={handleFirstBand}>
                                <MenuItem value='black'>Black</MenuItem>
                                <MenuItem value='brown'>Brown</MenuItem>
                                <MenuItem value='red'>Red</MenuItem>
                                <MenuItem value='orange'>Orange</MenuItem>
                                <MenuItem value='yellow'>Yellow</MenuItem>
                                <MenuItem value='green'>Green</MenuItem>
                                <MenuItem value='blue'>Blue</MenuItem>
                                <MenuItem value='violet'>Violet</MenuItem>
                                <MenuItem value='grey'>Grey</MenuItem>
                                <MenuItem value='white'>White</MenuItem>
                            </Select>
                        </div>
                        <div className='second-band-selector'>
                            <h4>2nd Band of Color</h4>
                            <Select className='select' labelId="demo-simple-select-label" id="demo-simple-select" value={secondBand} onChange={handleSecondBand}>
                                <MenuItem value='black'>Black</MenuItem>
                                <MenuItem value='brown'>Brown</MenuItem>
                                <MenuItem value='red'>Red</MenuItem>
                                <MenuItem value='orange'>Orange</MenuItem>
                                <MenuItem value='yellow'>Yellow</MenuItem>
                                <MenuItem value='green'>Green</MenuItem>
                                <MenuItem value='blue'>Blue</MenuItem>
                                <MenuItem value='violet'>Violet</MenuItem>
                                <MenuItem value='grey'>Grey</MenuItem>
                                <MenuItem value='white'>White</MenuItem>
                            </Select>
                        </div>
                        <div className='multiplier-selector'>
                            <h4>Multiplier</h4>
                            <Select className='select' labelId="demo-simple-select-label" id="demo-simple-select" value={multiplierBand} onChange={handleMultiplierBand}>
                                <MenuItem value='black'>Black</MenuItem>
                                <MenuItem value='brown'>Brown</MenuItem>
                                <MenuItem value='red'>Red</MenuItem>
                                <MenuItem value='orange'>Orange</MenuItem>
                                <MenuItem value='yellow'>Yellow</MenuItem>
                                <MenuItem value='green'>Green</MenuItem>
                                <MenuItem value='blue'>Blue</MenuItem>
                                <MenuItem value='violet'>Violet</MenuItem>
                                <MenuItem value='grey'>Grey</MenuItem>
                                <MenuItem value='white'>White</MenuItem>
                                <MenuItem value='gold'>Gold</MenuItem>
                                <MenuItem value='silver'>Silver</MenuItem>
                            </Select>
                        </div>
                        <div className='tolerance-selector'>
                            <h4>Tolerance</h4>
                            <Select className='select' labelId="demo-simple-select-label" id="demo-simple-select" value={toleranceBrand} onChange={handleToleranceBand}>
                                <MenuItem value='black'>Black</MenuItem>
                                <MenuItem value='brown'>Brown</MenuItem>
                                <MenuItem value='red'>Red</MenuItem>
                                <MenuItem value='orange'>Orange</MenuItem>
                                <MenuItem value='yellow'>Yellow</MenuItem>
                                <MenuItem value='green'>Green</MenuItem>
                                <MenuItem value='blue'>Blue</MenuItem>
                                <MenuItem value='violet'>Violet</MenuItem>
                                <MenuItem value='grey'>Grey</MenuItem>
                                <MenuItem value='white'>White</MenuItem>
                                <MenuItem value='gold'>Gold</MenuItem>
                                <MenuItem value='silver'>Silver</MenuItem>
                            </Select>
                        </div>
                </div>
                <div className='result'>
                    <div className='resistor-img'>
                        <img alt="resistance" src={resistance} />
                        <div className={firstBand ? `first-band ${firstBand}`: 'first-band'}></div>
                        <div className={secondBand ? `second-band ${secondBand}`: 'second-band'}></div>
                        <div className={multiplierBand ? `multiplier ${multiplierBand}`: 'multiplier'}></div>
                        <div className={toleranceBrand ? `tolerance ${toleranceBrand}`: 'tolerance'}></div>
                    </div>
                    <div className='resistance'>
                        <h2>Resistance</h2>
                        <h3>{ohmsValue} OHMS</h3>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Home