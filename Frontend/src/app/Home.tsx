"use client"

import PersonOutlined from "@mui/icons-material/PersonOutlined";
import LockOutlined from "@mui/icons-material/LockOutlined";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import WindowOutlined from "@mui/icons-material/WindowOutlined";
import LoginOutlined from "@mui/icons-material/LoginOutlined";
import BadgeOutlined from "@mui/icons-material/BadgeOutlined";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { Box } from "@mui/material";

export default function Home() {
    const router = useRouter()
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [showPassword, setShowPassword] = useState(false)
    const [error, setError] = useState<string | null>(null)
    const [loadingManual, setLoadingManual] = useState(false)
    const [loadingSSO, setLoadingSSO] = useState(false)

    // Manual login

    return (
        <Box></Box>
    )
}