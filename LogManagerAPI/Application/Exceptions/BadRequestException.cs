
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message, params object[] args)
        : base(message, HttpStatusCode.BadRequest, args) { }

    public BadRequestException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.BadRequest, inner, args) { }
}