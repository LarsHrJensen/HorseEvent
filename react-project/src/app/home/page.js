'use client'
import "./page.css"

export default function HomePage() {
    return(
        <main className="flex flex-col items-center justify-center min-h-screen space-y-6">
            <div className="flex space-x-6">
                <h1 className="text-4xl font-bold mb-4 text-center text-top text-[#243C80]"> 
                    Welcome back, User! 
                </h1>
                <button type="button" className="btn-default"> Stævner </button>
                <button type="buton" className="btn-default btn-default--p"> Hjem </button>
            </div>
            
            <div className="flex space-x-6">
                <div className="box-wrapper box-wrapper--small">
                    <h1 className="text-2xl font-bold mb-4 text-center text-top text-[#1B2D5E]">Competition overview</h1>
                </div>

                <div className="box-wrapper box-wrapper--small">
                    <p className="text-[#1B2D5E]">Competition Status</p>
                </div>

                <div className="box-wrapper box-wrapper--small space-y-1">
                    <p className="text-[#1B2D5E]">Quick Actions</p>
                    <button className="btn-default">Maayybee</button>
                    <button className="btn-default">Maayybee</button>
                    <button className="btn-default">Maayybee</button>
                    <button className="btn-default btn-default--p">Maayybee</button>
                </div>
            </div>

            <div>
                <div className="box-wrapper box-wrapper--info flex justify-between items-center w-full">
                    <p>competition progess</p>
                    <div className="flex gap-x-4">
                        <button className="btn-default btn-default--w text-[#8e44ad]"> Se Live Opdateringer</button>
                        <button className="btn-default btn-default--w text-[#8e44ad]"> Download Tidsplan</button>
                    </div>
                </div>
            </div>

            <div className="flex space-x-6">
                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">participants</p>
                </div>

                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">schedule</p>
                </div>
            </div>

            <div className="flex space-x-6">
                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">results</p>
                </div>

                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">judges & officials</p>
                </div>
            </div>
            
        </main> 
    )
}