import axios from 'axios'

const url = 'https://shipcomtest.azurewebsites.net/api/ohm?'

export async function getResistanceOHMSValueService (firstBand, secondBand, multiplierBand, toleranceBrand) {
    try {
        const result = await axios.get(`${url}bandAColor=${firstBand}&bandBColor=${secondBand}&bandCColor=${multiplierBand}&bandDColor=${toleranceBrand}`)
        return result.data.result
    } catch(e) {
        console.log('Error',e)
    }
}