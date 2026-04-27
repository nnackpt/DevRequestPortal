"use client"

import { Poppins } from "next/font/google"
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material"

const poppins = Poppins({
    subsets: ["latin"],
    weight: ["300", "400", "500", "600", "700"],
    display: "swap"
})

const theme = createTheme({
    palette: {
        primary: { main: "#164799" },
        background: { default: "#f5f7fa" }
    },
    typography: {
        fontFamily: poppins.style.fontFamily
    },
    components: {
        MuiButton: {
            styleOverrides: {
                root: {
                    borderRadius: 8,
                    textTransform: "none",
                    fontWeight: 600
                }
            }
        },
        MuiOutlinedInput: {
            styleOverrides: {
                root: {
                    borderRadius: 8
                }
            }
        }
    }
})

export default function Providers({ children }: { children: React.ReactNode }) {
    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <div className={poppins.className}>{children}</div>
        </ThemeProvider>
    )
}