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
                    <p className="text-[#1B2D5E]"> Competition overview</p>
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


            <div className="box-wrapper box-wrapper--info">
                <p>Competition Progess</p>
                <div className="flex gap-x-4">
                    <button className="btn-default btn-default--w text-[#8e44ad]"> Live Opdateringer</button>
                    <button className="btn-default btn-default--w text-[#8e44ad]"> Download Tidsplan</button>
                </div>
            </div>

            <div className="flex space-x-6">
                <div className="box-wrapper box-wrapper--with-topbar">
                    <div className="box-topbar">
                        <h2>Participants</h2>
                        <div className="actions">
                            <input type="text" 
                                placeholder="Search participants..."></input>
                            <button className="btn-small text-[#4a90e2]"> Filtrer </button>
                        </div>
                    </div>

                    <div className="box-content">
                        <p> Participant info or something...</p>
                    </div>
                </div>

                <div className="box-wrapper box-wrapper--with-topbar">
                    <div className="box-topbar">
                        <h2>Schedule</h2>
                        <div className="actions">
                            <button className="btn-small text-[#4a90e2]"> Add Event </button>
                        </div>
                    </div>
                </div>
            </div>

            <div className="flex space-x-6">
                <div className="box-wrapper box-wrapper--with-topbar">
                    <div className="box-topbar">
                        <h2 className="text-[#1B2D5E]"> Results </h2>
                        <div className="actions">
                            <button className="btn-small text-[#4a90e2]"> Export </button>
                        </div>
                    </div>
                </div>

                <div className="box-wrapper box-wrapper--with-topbar">
                    <div className="box-topbar">
                        <h2>Judges & Officials</h2>
                        <div className="actions">
                            <button className="btn-small text-[#4a90e2]"> Add Judge </button>
                        </div>
                    </div>
                </div>
            </div>
            
        </main> 
    )
}